using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SocialPulseInsightHub
{
    public class SocialMediaAccount
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public List<SocialMediaPlatform> Platforms { get; }
        public string AuthenticationToken { get; set; }

        public SocialMediaAccount(string userName)
        {
            UserName = userName;
            Platforms = new List<SocialMediaPlatform>();
            AuthenticationToken = "token comes here";
        }

        public void AuthenticateUser(string name, string inputPassword, string storedSalt, string storedHash)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(inputPassword, Convert.FromBase64String(storedSalt), 1000, HashAlgorithmName.SHA256))
            {
                string computedHash = Convert.ToBase64String(deriveBytes.GetBytes(32));

                if (UserName.Equals(name) && computedHash.Equals(storedHash))
                {
                    Debug.WriteLine("User authenticated successfully.");
                }
                else
                {
                    Debug.WriteLine("ERROR: Username or password are not correct.");
                }
            }
        }

        public void FetchData(string endpoint)
        {
            // API fetching logic goes here
        }

        public void AssociatePlatform(SocialMediaPlatform platform)
        {
            Platforms.Add(platform);
        }

        public void RemovePlatform(SocialMediaPlatform platform)
        {
            Platforms.Remove(platform);
        }
    }
}
