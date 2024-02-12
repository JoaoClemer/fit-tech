using FitTech.Application.Services.Cryptography;

namespace FitTech.Tests.Utils.Repositories.Services
{
    public class PasswordEncryptorBuilder
    {
        public static PasswordEncryptor Instance()
        {
            return new PasswordEncryptor("uHPqx4*K&fN!");
        }
    }
}
