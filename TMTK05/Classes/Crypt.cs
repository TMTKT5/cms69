#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace TMTK05.Classes
{
    public static class Crypt
    {
        #region Public Methods

        // <summary>
        // Create MD5 hash
        // </summary>
        public static String GetMd5Hash(MD5 md5Hash, String input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        // <summary>
        // Create a random salt
        // </summary>
        public static String GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        // <summary>
        // Hash a string using a random salt
        // </summary>
        public static String HashPassword(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        ///     Decrypt a string using rijdeal
        /// </summary>
        public static string StringDecrypt(string cipherText, string password)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var pdb = new PasswordDeriveBytes(password,
                new byte[] {0x51, 0x42, 0x69, 0x2e, 0x4f, 0x3a, 0x56, 0x59, 0x16, 0x3c, 0xcd, 0x4d, 0x36});
#pragma warning disable 618
            var decryptedData = StringDecrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
#pragma warning restore 618

            return Encoding.Unicode.GetString(decryptedData);
        }

        // <summary>
        // Encrypt a string using rijdeal
        // </summary>
        public static string StringEncrypt(string clearText, string password)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            var pdb = new PasswordDeriveBytes(password,
                new byte[] {0x51, 0x42, 0x69, 0x2e, 0x4f, 0x3a, 0x56, 0x59, 0x16, 0x3c, 0xcd, 0x4d, 0x36});
#pragma warning disable 618
            var encryptedData = StringEncrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
#pragma warning restore 618

            return Convert.ToBase64String(encryptedData);
        }

        // <summary>
        // Check is the database hash and the hash of the givin string are the same
        // </summary>
        public static Boolean ValidatePassword(String enteredPassword, String storedHash, String storedSalt)
        {
            var hash = HashPassword(enteredPassword, storedSalt);

            return String.Equals(storedHash, hash);
        }

        // <summary>
        // Check if given hash and a hashed string are the same
        // </summary>
        public static Boolean VerifyMd5Hash(MD5 md5Hash, String input, String hash)
        {
            var hashOfInput = GetMd5Hash(md5Hash, input);
            var comparer = StringComparer.OrdinalIgnoreCase;

            return 0 == comparer.Compare(hashOfInput, hash);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Decrypt a string using rijdeal
        /// </summary>
        private static byte[] StringDecrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            var decryptedData = ms.ToArray();

            return decryptedData;
        }

        // <summary>
        // Encrypt a string using rijdeal
        // </summary>
        private static byte[] StringEncrypt(byte[] clearText, byte[] key, byte[] iv)
        {
            var ms = new MemoryStream();
            var alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;
            var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearText, 0, clearText.Length);
            cs.Close();
            var encryptedData = ms.ToArray();

            return encryptedData;
        }

        #endregion Private Methods
    }
}