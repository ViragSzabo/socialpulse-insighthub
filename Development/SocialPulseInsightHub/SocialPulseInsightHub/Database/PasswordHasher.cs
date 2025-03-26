using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialPulseInsightHub.Database
{
    public class PasswordHasher
    {
        public static (string hash, string salt) HashPassword(string password)
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            string salt = Convert.ToBase64String(saltBytes);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000, HashAlgorithmName.SHA256))
            {
                string hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
                return (hash, salt);
            }
        }
    }
}