using System;
using System.Security.Cryptography;
using System.Text;

namespace ArmourySystem
{
    public static class CryptoHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567812345678"); // 16 bytes
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("8765432187654321");  // 16 bytes

        public static string EncryptString(string plainText)
        {
            Aes aesCipher = Aes.Create();

            // Replace 'using declaration' with a traditional 'using statement' to ensure compatibility with C# 7.3
            using (Aes aes = aesCipher)
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encrypted);
            }
        }

        public static string HashPassword(string password)
        {
            SHA256 sHA256 = SHA256.Create();

            // Replace 'using declaration' with a traditional 'using statement' to ensure compatibility with C# 7.3
            using (SHA256 sha256 = sHA256)
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                return Convert.ToBase64String(hashBytes); // you can also return BitConverter.ToString(hashBytes).Replace("-", "") for hex
            }
        }

        public static string HashPasswordWithSalt(string password, string salt)
        {
            return HashPassword(password + salt);
        }

        public static string DecryptString(string encryptedText)
        {
            Aes aesCipher = Aes.Create();
            // Replace 'using declaration' with a traditional 'using statement' to ensure compatibility with C# 7.3
            using (Aes aes = aesCipher)
            {
                aes.Key = Key;
                aes.IV = IV;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] inputBytes = Convert.FromBase64String(encryptedText);
                byte[] decrypted = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Encoding.UTF8.GetString(decrypted);
            }
        }

        public static string GenerateSalt(int length = 16)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[length];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes); // you can also return BitConverter.ToString(saltBytes).Replace("-", "") for hex
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            if (salt != null)
            {
                password += salt;
            }
            string hashedInput = HashPassword(password);
            return hashedInput.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}
