using PersonalDigitalVault_WebApplication.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace PersonalDigitalVault_WebApplication.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly byte[] _key;

        public EncryptionService(IConfiguration configuration)
        {
            // 32 bytes key for AES-256 (simple student-project approach)
            var keyText = configuration["Encryption:Key"]
                ?? "PersonalDigitalVault_AES_Key_2026!";

            _key = Encoding.UTF8.GetBytes(keyText.PadRight(32).Substring(0, 32));
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            // IV + cipher text stored together
            var result = new byte[aes.IV.Length + cipherBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(cipherBytes, 0, result, aes.IV.Length, cipherBytes.Length);

            return Convert.ToBase64String(result);
        }

        public string Decrypt(string cipherText)
        {
            var fullBytes = Convert.FromBase64String(cipherText);

            using var aes = Aes.Create();
            aes.Key = _key;

            var iv = new byte[aes.BlockSize / 8];
            var cipherBytes = new byte[fullBytes.Length - iv.Length];

            Buffer.BlockCopy(fullBytes, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullBytes, iv.Length, cipherBytes, 0, cipherBytes.Length);

            aes.IV = iv;

            using var decryptor = aes.CreateDecryptor();
            var plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(plainBytes);
        }
    }
}
