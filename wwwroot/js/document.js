document.addEventListener("DOMContentLoaded", async () => {
  await hydrateShell("documents");

  const form = document.getElementById("uploadForm");
  const alertBox = document.getElementById("documentAlert");
  const tbody = document.getElementById("documentTableBody");
  const folderSelect = document.getElementById("folderId");
  const filterSelect = document.getElementById("filterFolder");

  async function loadFolderOptions() {
    const folders = await VaultApi.getFolders();
    const options =
      folders?.map(
        (f) => `<option value="${f.folderId}">${escapeHtml(f.folderName)}</option>`
      ) || [];
    const html = options.join("") || `<option value="">No folders</option>`;
    if (folderSelect) folderSelect.innerHTML = html;
    if (filterSelect) {
      filterSelect.innerHTML = `<option value="">All folders</option>${options.join("")}`;
    }
  }

  async function loadDocuments() {
    tbody.innerHTML = `<tr><td colspan="5" class="empty">Loading…</td></tr>`;
    try {
      const folderId = filterSelect?.value;
      const docs = folderId
        ? await VaultApi.getDocumentsByFolder(folderId)
        : await VaultApi.getDocuments();

      if (!docs?.length) {
        tbody.innerHTML = `<tr><td colspan="5" class="empty">No documents found.</td></tr>`;
        return;
      }

      tbody.innerHTML = docs
        .map(
          (d) => `
        <tr>
          <td>${d.documentId}</td>
          <td class="file-name">${escapeHtml(d.fileName)}</td>
          <td>${d.folderId}</td>
          <td class="file-name">${escapeHtml((d.fileHash || "").slice(0, 16))}…</td>
          <td class="actions">
            <button class="btn btn-ghost btn-sm" data-dl="${d.documentId}" data-name="${escapeAttr(d.fileName)}">Download</button>
            <button class="btn btn-danger btn-sm" data-del="${d.documentId}">Delete</button>
          </td>
        </tr>`
        )
        .join("");
    } catch (err) {
      tbody.innerHTML = `<tr><td colspan="5" class="empty">${escapeHtml(err.message)}</td></tr>`;
    }
  }

  form?.addEventListener("submit", async (e) => {
    e.preventDefault();
    hideAlert(alertBox);

    const file = form.file.files[0];
    if (!file) {
      showAlert(alertBox, "Choose a file to upload");
      return;
    }

    const data = new FormData();
    data.append("folderId", form.folderId.value);
    data.append("file", file);

    try {
      await VaultApi.uploadDocument(data);
      form.file.value = "";
      showAlert(alertBox, "Document uploaded", "ok");
      await loadDocuments();
    } catch (err) {
      showAlert(alertBox, err.message || "Upload failed");
    }
  });

  filterSelect?.addEventListener("change", loadDocuments);

  tbody?.addEventListener("click", async (e) => {
    const dl = e.target.closest("[data-dl]");
    const del = e.target.closest("[data-del]");

    if (dl) {
      try {
        const blob = await VaultApi.downloadDocument(dl.dataset.dl);
        const url = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = url;
        a.download = dl.dataset.name || "download";
        a.click();
        URL.revokeObjectURL(url);
      } catch (err) {
        showAlert(alertBox, err.message || "Download failed");
      }
    }

    if (del) {
      if (!confirm("Delete this document?")) return;
      try {
        await VaultApi.deleteDocument(del.dataset.del);
        showAlert(alertBox, "Document deleted", "ok");
        await loadDocuments();
      } catch (err) {
        showAlert(alertBox, err.message || "Delete failed");
      }
    }
  });

  try {
    await loadFolderOptions();
    await loadDocuments();
  } catch (err) {
    showAlert(alertBox, err.message || "Failed to load data");
  }
});

function escapeHtml(value) {
  return String(value ?? "")
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;");
}

function escapeAttr(value) {
  return escapeHtml(value).replace(/'/g, "&#39;");
}
