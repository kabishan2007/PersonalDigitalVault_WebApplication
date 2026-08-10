document.addEventListener("DOMContentLoaded", async () => {
  const profile = await hydrateShell("profile");
  const form = document.getElementById("profileForm");
  const alertBox = document.getElementById("profileAlert");

  if (profile && form) {
    form.name.value = profile.name || "";
    form.email.value = profile.email || "";
    form.role.value = profile.role || "";
  }

  form?.addEventListener("submit", async (e) => {
    e.preventDefault();
    hideAlert(alertBox);

    try {
      await VaultApi.updateProfile({
        name: form.name.value.trim(),
        email: form.email.value.trim(),
        role: form.role.value,
        userId: 0,
      });
      showAlert(alertBox, "Profile updated successfully", "ok");
      await hydrateShell("profile");
    } catch (err) {
      showAlert(alertBox, err.message || "Update failed");
    }
  });
});
