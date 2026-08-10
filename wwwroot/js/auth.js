document.addEventListener("DOMContentLoaded", () => {
  if (VaultApi.getToken() && (location.pathname.endsWith("login.html") || location.pathname.endsWith("register.html") || location.pathname.endsWith("index.html") || location.pathname === "/")) {
    window.location.href = "/dashboard.html";
    return;
  }

  const loginForm = document.getElementById("loginForm");
  const registerForm = document.getElementById("registerForm");
  const alertBox = document.getElementById("authAlert");

  if (loginForm) {
    loginForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      hideAlert(alertBox);

      const email = loginForm.email.value.trim();
      const password = loginForm.password.value;

      try {
        const result = await VaultApi.login({ email, password });
        VaultApi.setToken(result.token);
        showAlert(alertBox, "Login successful. Opening vault…", "ok");
        setTimeout(() => {
          window.location.href = "/dashboard.html";
        }, 400);
      } catch (err) {
        showAlert(alertBox, err.message || "Login failed");
      }
    });
  }

  if (registerForm) {
    registerForm.addEventListener("submit", async (e) => {
      e.preventDefault();
      hideAlert(alertBox);

      const name = registerForm.name.value.trim();
      const email = registerForm.email.value.trim();
      const password = registerForm.password.value;

      if (password.length < 6) {
        showAlert(alertBox, "Password must be at least 6 characters");
        return;
      }

      try {
        await VaultApi.register({ name, email, password });
        showAlert(alertBox, "Registered successfully. Please login.", "ok");
        setTimeout(() => {
          window.location.href = "/login.html";
        }, 700);
      } catch (err) {
        showAlert(alertBox, err.message || "Registration failed");
      }
    });
  }
});
