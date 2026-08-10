document.addEventListener("DOMContentLoaded", async () => {
  await hydrateShell("folders");

  const form = document.getElementById("folderForm");
  const alertBox = document.getElementById("folderAlert");
  const tbody = document.getElementById("folderTableBody");
  const modal = document.getElementById("editModal");
  const editForm = document.getElementById("editFolderForm");
  let editingId = null;

  async function loadFolders() {
    tbody.innerHTML = `<tr><td colspan="3" class="empty">Loading…</td></tr>`;
    try {
      const folders = await VaultApi.getFolders();
      if (!folders?.length) {
        tbody.innerHTML = `<tr><td colspan="3" class="empty">No folders yet. Create one above.</td></tr>`;
        return;
      }

      tbody.innerHTML = folders
        .map(
          (f) => `
        <tr>
          <td>${f.folderId}</td>
          <td>${escapeHtml(f.folderName)}</td>
          <td class="actions">
            <button class="btn btn-ghost btn-sm" data-edit="${f.folderId}" data-name="${escapeAttr(f.folderName)}">Edit</button>
            <button class="btn btn-danger btn-sm" data-del="${f.folderId}">Delete</button>
          </td>
        </tr>`
        )
        .join("");
    } catch (err) {
      tbody.innerHTML = `<tr><td colspan="3" class="empty">${escapeHtml(err.message)}</td></tr>`;
    }
  }

  form?.addEventListener("submit", async (e) => {
    e.preventDefault();
    hideAlert(alertBox);
    try {
      await VaultApi.createFolder({ folderName: form.folderName.value.trim() });
      form.reset();
      showAlert(alertBox, "Folder created", "ok");
      await loadFolders();
    } catch (err) {
      showAlert(alertBox, err.message || "Create failed");
    }
  });

  tbody?.addEventListener("click", async (e) => {
    const editBtn = e.target.closest("[data-edit]");
    const delBtn = e.target.closest("[data-del]");

    if (editBtn) {
      editingId = editBtn.dataset.edit;
      editForm.folderName.value = editBtn.dataset.name || "";
      modal.classList.add("show");
    }

    if (delBtn) {
      if (!confirm("Delete this folder?")) return;
      hideAlert(alertBox);
      try {
        await VaultApi.deleteFolder(delBtn.dataset.del);
        showAlert(alertBox, "Folder deleted", "ok");
        await loadFolders();
      } catch (err) {
        showAlert(alertBox, err.message || "Delete failed");
      }
    }
  });

  document.getElementById("closeModal")?.addEventListener("click", () => {
    modal.classList.remove("show");
  });

  editForm?.addEventListener("submit", async (e) => {
    e.preventDefault();
    if (!editingId) return;
    try {
      await VaultApi.updateFolder(editingId, {
        folderName: editForm.folderName.value.trim(),
      });
      modal.classList.remove("show");
      showAlert(alertBox, "Folder updated", "ok");
      await loadFolders();
    } catch (err) {
      showAlert(alertBox, err.message || "Update failed");
    }
  });

  await loadFolders();
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
