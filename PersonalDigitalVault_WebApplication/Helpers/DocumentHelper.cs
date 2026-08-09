using System.Security.Cryptography;

namespace PersonalDigitalVault_WebApplication.Helpers
{
    public class DocumentHelper
    {
        public static string GenerateSHA256(byte[] fileBytes)
        {
            using var sha = SHA256.Create();

            var hashBytes = sha.ComputeHash(fileBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}
