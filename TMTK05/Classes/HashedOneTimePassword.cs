#region

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace TMTK05.Classes
{
    public static class HashedOneTimePassword
    {
        #region Public Methods

        public static string GeneratePassword(string secret, long iterationNumber, int digits = 6)
        {
            var counter = BitConverter.GetBytes(iterationNumber);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(counter);

            var key = Encoding.ASCII.GetBytes(secret);

            var hmac = new HMACSHA1(key, true);

            var hash = hmac.ComputeHash(counter);

            var offset = hash[hash.Length - 1] & 0xf;

            // Convert the 4 bytes into an integer, ignoring the sign. 
            var binary =
                ((hash[offset] & 0x7f) << 24)
                | (hash[offset + 1] << 16)
                | (hash[offset + 2] << 8)
                | (hash[offset + 3]);

            // Limit the number of digits 
            var password = binary%(int) Math.Pow(10, digits);

            // Pad to required digits 
            return password.ToString(new string('0', digits));
        }

        #endregion Public Methods
    }
}