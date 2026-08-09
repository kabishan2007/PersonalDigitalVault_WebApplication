namespace PersonalDigitalVault_WebApplication.DTOs.Credential
{
    public class CredentialDto
    {
        public int CredentialId { get; set; }

        public string SiteName { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
