document.addEventListener("DOMContentLoaded", async () => {
  const profile = await hydrateShell("admin");
  const alertBox = document.getElementById("adminAlert");
  const tbody = document.getElementById("usersTableBody");

  if (profile && profile.role !== "Admin" && VaultApi.getRole() !== "Admin") {
    showAlert(alertBox, "Admin access only. Redirecting…");
    setTimeout(() => (window.location.href = "/dashboard.html"), 900);
    return;
  }

  try {
    const dash = await VaultApi.adminDashboard();
    setText("statUsers", dash.totalUsers);
    setText("statFolders", dash.totalFolders);
    setText("statDocuments", dash.totalDocuments);
    setText("statCredentials", dash.totalCredentials);

    const users = await VaultApi.adminUsers();
    if (!users?.length) {
      tbody.innerHTML = `<tr><td colspan="4" class="empty">No users found.</td></tr>`;
      return;
    }

    tbody.innerHTML = users
      .map(
        (u) => `
      <tr>
        <td>${u.userId}</td>
        <td>${escapeHtml(u.name)}</td>
        <td>${escapeHtml(u.email)}</td>
        <td><span class="badge ${u.role === "Admin" ? "role-admin" : ""}">${escapeHtml(u.role)}</span></td>
      </tr>`
      )
      .join("");
  } catch (err) {
    showAlert(alertBox, err.message || "Failed to load admin data");
    tbody.innerHTML = `<tr><td colspan="4" class="empty">${escapeHtml(err.message)}</td></tr>`;
  }
});

function setText(id, value) {
  const el = document.getElementById(id);
  if (el) el.textContent = value ?? 0;
}

function escapeHtml(value) {
  return String(value ?? "")
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;");
}
