using System.Security.Cryptography;
using System.Text;

namespace FitTech.Application.Services.Cryptography
{
    public class PasswordEncryptor
    {
        private readonly string _encryptKey;

        public PasswordEncryptor(string encryptKey)
        {
            _encryptKey = encryptKey;
        }
        public string Encrypt(string password)
        {
            var passwordWithEncryptKey = $"{password}{_encryptKey}";

            var bytes = Encoding.UTF8.GetBytes(passwordWithEncryptKey);
            var sha512 = SHA512.Create();
            byte[] hasBytes = sha512.ComputeHash(bytes);
            return StringBytes(hasBytes);
        }

        private string StringBytes(byte[] hasBytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in hasBytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
