const VaultApi = (() => {
  const TOKEN_KEY = "pdv_token";

  function getToken() {
    return localStorage.getItem(TOKEN_KEY);
  }

  function setToken(token) {
    localStorage.setItem(TOKEN_KEY, token);
  }

  function clearAuth() {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem("pdv_profile");
  }

  function parseJwt(token) {
    try {
      const payload = token.split(".")[1];
      const base64 = payload.replace(/-/g, "+").replace(/_/g, "/");
      const json = decodeURIComponent(
        atob(base64)
          .split("")
          .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
          .join("")
      );
      return JSON.parse(json);
    } catch {
      return null;
    }
  }

  function getRole() {
    const token = getToken();
    if (!token) return null;
    const data = parseJwt(token);
    if (!data) return null;
    return (
      data.role ||
      data["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ||
      null
    );
  }

  function requireAuth() {
    if (!getToken()) {
      window.location.href = "/login.html";
      return false;
    }
    return true;
  }

  function logout() {
    clearAuth();
    window.location.href = "/login.html";
  }

  async function request(path, options = {}) {
    const headers = new Headers(options.headers || {});
    const token = getToken();
    const isForm = options.body instanceof FormData;

    if (!isForm && options.body && !headers.has("Content-Type")) {
      headers.set("Content-Type", "application/json");
    }
    if (token) {
      headers.set("Authorization", `Bearer ${token}`);
    }

    const response = await fetch(path, { ...options, headers });

    if (response.status === 401) {
      clearAuth();
      if (!path.toLowerCase().includes("/auth/")) {
        window.location.href = "/login.html";
      }
    }

    const contentType = response.headers.get("content-type") || "";
    let data = null;

    if (contentType.includes("application/json")) {
      data = await response.json();
    } else if (options.raw) {
      data = await response.blob();
    } else {
      const text = await response.text();
      try {
        data = JSON.parse(text);
      } catch {
        data = text;
      }
    }

    if (!response.ok) {
      const message = friendlyError(data, response.status);
      const error = new Error(message);
      error.status = response.status;
      error.data = data;
      throw error;
    }

    return data;
  }

  return {
    getToken,
    setToken,
    clearAuth,
    getRole,
    requireAuth,
    logout,
    request,
    parseJwt,

    register: (body) =>
      request("/Auth/register", { method: "POST", body: JSON.stringify(body) }),
    login: (body) =>
      request("/Auth/login", { method: "POST", body: JSON.stringify(body) }),

    getProfile: () => request("/Profile"),
    updateProfile: (body) =>
      request("/Profile", { method: "PUT", body: JSON.stringify(body) }),

    getFolders: () => request("/Folders"),
    getFolder: (id) => request(`/Folders/${id}`),
    createFolder: (body) =>
      request("/Folders", { method: "POST", body: JSON.stringify(body) }),
    updateFolder: (id, body) =>
      request(`/Folders/${id}`, { method: "PUT", body: JSON.stringify(body) }),
    deleteFolder: (id) => request(`/Folders/${id}`, { method: "DELETE" }),

    getDocuments: () => request("/Documents"),
    getDocumentsByFolder: (folderId) =>
      request(`/Documents/folder/${folderId}`),
    uploadDocument: (formData) =>
      request("/Documents", { method: "POST", body: formData }),
    deleteDocument: (id) => request(`/Documents/${id}`, { method: "DELETE" }),
    downloadDocument: (id) =>
      request(`/Documents/${id}/download`, { raw: true }),

    getCredentials: () => request("/Credentials"),
    createCredential: (body) =>
      request("/Credentials", { method: "POST", body: JSON.stringify(body) }),
    updateCredential: (id, body) =>
      request(`/Credentials/${id}`, {
        method: "PUT",
        body: JSON.stringify(body),
      }),
    deleteCredential: (id) =>
      request(`/Credentials/${id}`, { method: "DELETE" }),

    adminDashboard: () => request("/Admin/dashboard"),
    adminUsers: () => request("/Admin/users"),
  };
})();

function friendlyError(data, status) {
  if (data && typeof data === "object" && data.message) {
    return String(data.message);
  }

  const text = typeof data === "string" ? data : "";
  if (!text) return `Request failed (${status})`;

  if (/Cannot open database/i.test(text)) {
    return "Database is not ready. Restart the app so tables can be created.";
  }
  if (/Login failed/i.test(text)) {
    return "Database login failed. Check SQL Server / LocalDB connection.";
  }
  if (/CS0579|Duplicate '.*Attribute'/i.test(text)) {
    return "Server build issue detected. Stop the app, delete bin/obj, then run again.";
  }

  const firstLine = text
    .split(/\r?\n/)
    .map((l) => l.trim())
    .find((l) => l && !l.startsWith("HEADERS"));

  if (firstLine && firstLine.length > 180) {
    return firstLine.slice(0, 180) + "…";
  }

  return firstLine || `Request failed (${status})`;
}

function showAlert(el, message, type = "error") {
  if (!el) return;
  el.textContent = message;
  el.className = `alert show alert-${type === "ok" ? "ok" : "error"}`;
}

function hideAlert(el) {
  if (!el) return;
  el.className = "alert";
  el.textContent = "";
}

async function hydrateShell(active) {
  if (!VaultApi.requireAuth()) return null;

  const nameEl = document.getElementById("shellUserName");
  const roleEl = document.getElementById("shellUserRole");
  const adminLink = document.getElementById("adminNav");
  const logoutBtn = document.getElementById("logoutBtn");

  document.querySelectorAll(".nav-links a").forEach((a) => {
    if (a.dataset.nav === active) a.classList.add("active");
  });

  if (logoutBtn) {
    logoutBtn.addEventListener("click", () => VaultApi.logout());
  }

  try {
    const profile = await VaultApi.getProfile();
    localStorage.setItem("pdv_profile", JSON.stringify(profile));
    if (nameEl) nameEl.textContent = profile.name || "User";
    if (roleEl) roleEl.textContent = profile.role || "User";
    if (adminLink) {
      adminLink.style.display =
        profile.role === "Admin" || VaultApi.getRole() === "Admin"
          ? "block"
          : "none";
    }
    return profile;
  } catch (err) {
    if (nameEl) nameEl.textContent = "User";
    if (roleEl) roleEl.textContent = VaultApi.getRole() || "User";
    if (adminLink && VaultApi.getRole() === "Admin") {
      adminLink.style.display = "block";
    }
    return null;
  }
}

function shellHtml() {
  return "";
}
