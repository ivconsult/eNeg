

#region → Usings   .
using System;
using System.Text;
using System.Security.Cryptography;
#endregion

#region → History  .

/* Date         User            change
 * 
 * 31.08.10     M.Wahab     creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion


namespace citPOINT.eNeg.Common
{
    /// <summary>
    /// Used To Encrypt Decrypt
    /// </summary>
    public class HashHelper
    {

        #region → Methods        .

        #region Satic Public
        /// <summary>
        /// Create random salt
        /// </summary>
        /// <returns>Created salt</returns>
        public static string CreateRandomSalt()
        {
            Byte[] saltBytes = new Byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// Compute salted password hash
        /// </summary>
        /// <param name="password">Value Of password</param>
        /// <param name="salt">Value Of salt</param>
        /// <returns>Created salted hash</returns>
        public static string ComputeSaltedHash(string password, string salt)
        {
            // Create Byte array of password string
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] secretBytes = encoder.GetBytes(password);

            // Create a new salt
            Byte[] saltBytes = Convert.FromBase64String(salt);

            // append the two arrays
            Byte[] toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            System.Security.Cryptography.HMACSHA256 hMACSHA256 = new HMACSHA256(saltBytes);
            return Convert.ToBase64String(hMACSHA256.ComputeHash(toHash));

        }
        
        /// <summary>
        /// Compute salted password hash
        /// </summary>
        /// <param name="password">Value Of password</param>
        /// <returns>Created salted hash</returns>
        public static string ComputeSaltedHash(string password)
        {
            string salt = CreateRandomSalt();
            // Create Byte array of password string
            UnicodeEncoding encoder = new UnicodeEncoding();
            Byte[] secretBytes = encoder.GetBytes(password);

            // Create a new salt
            Byte[] saltBytes = Convert.FromBase64String(salt);

            // append the two arrays
            Byte[] toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            System.Security.Cryptography.HMACSHA256 hMACSHA256 = new HMACSHA256(saltBytes);
            return Convert.ToBase64String(hMACSHA256.ComputeHash(toHash)).Replace("=", "");
          

        }

        #endregion

        #endregion
    }
}
