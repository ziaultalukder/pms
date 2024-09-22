using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace PMS.Helpers
{
    public class PasswordHelper
    {
        public static KeyValuePair<string, string> HashPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var saltString = Convert.ToBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return new KeyValuePair<string, string>(saltString, hashed);
        }

        /// <summary>
        /// create hash of old password with password salt
        /// </summary>
        /// <param name="providedPassword"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public static string HashPassword(string providedPassword, string passwordSalt)
        {
            // generate a 128-bit salt using a secure PRNG

            var saltByte = Convert.FromBase64String(passwordSalt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: providedPassword,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
