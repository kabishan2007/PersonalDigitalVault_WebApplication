document.addEventListener("DOMContentLoaded", async () => {
  await hydrateShell("credentials");

  const form = document.getElementById("credentialForm");
  const alertBox = document.getElementById("credentialAlert");
  const tbody = document.getElementById("credentialTableBody");
  const modal = document.getElementById("editModal");
  const editForm = document.getElementById("editCredentialForm");
  let editingId = null;

  async function loadCredentials() {
    tbody.innerHTML = `<tr><td colspan="5" class="empty">Loading…</td></tr>`;
    try {
      const items = await VaultApi.getCredentials();
      if (!items?.length) {
        tbody.innerHTML = `<tr><td colspan="5" class="empty">No credentials saved yet.</td></tr>`;
        return;
      }

      tbody.innerHTML = items
        .map(
          (c) => `
        <tr>
          <td>${c.credentialId}</td>
          <td>${escapeHtml(c.siteName)}</td>
          <td>${escapeHtml(c.username)}</td>
          <td class="password-cell">${escapeHtml(c.password)}</td>
          <td class="actions">
            <button class="btn btn-ghost btn-sm"
              data-edit="${c.credentialId}"
              data-site="${escapeAttr(c.siteName)}"
              data-user="${escapeAttr(c.username)}"
              data-pass="${escapeAttr(c.password)}">Edit</button>
            <button class="btn btn-danger btn-sm" data-del="${c.credentialId}">Delete</button>
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
    try {
      await VaultApi.createCredential({
        siteName: form.siteName.value.trim(),
        username: form.username.value.trim(),
        password: form.password.value,
      });
      form.reset();
      showAlert(alertBox, "Credential saved", "ok");
      await loadCredentials();
    } catch (err) {
      showAlert(alertBox, err.message || "Save failed");
    }
  });

  tbody?.addEventListener("click", async (e) => {
    const editBtn = e.target.closest("[data-edit]");
    const delBtn = e.target.closest("[data-del]");

    if (editBtn) {
      editingId = editBtn.dataset.edit;
      editForm.siteName.value = editBtn.dataset.site || "";
      editForm.username.value = editBtn.dataset.user || "";
      editForm.password.value = editBtn.dataset.pass || "";
      modal.classList.add("show");
    }

    if (delBtn) {
      if (!confirm("Delete this credential?")) return;
      try {
        await VaultApi.deleteCredential(delBtn.dataset.del);
        showAlert(alertBox, "Credential deleted", "ok");
        await loadCredentials();
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
      await VaultApi.updateCredential(editingId, {
        siteName: editForm.siteName.value.trim(),
        username: editForm.username.value.trim(),
        password: editForm.password.value,
      });
      modal.classList.remove("show");
      showAlert(alertBox, "Credential updated", "ok");
      await loadCredentials();
    } catch (err) {
      showAlert(alertBox, err.message || "Update failed");
    }
  });

  await loadCredentials();
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
