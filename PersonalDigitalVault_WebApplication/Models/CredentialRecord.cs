using System.ComponentModel.DataAnnotations;

namespace PersonalDigitalVault_WebApplication.Models
{
    public class CredentialRecord
    {
        [Key]
        public int CredentialId { get; set; }

        public string SiteName { get; set; }

        public string Username { get; set; }

        public string EncryptedPassword { get; set; }

        public int UserId { get; set; }
    }
}

