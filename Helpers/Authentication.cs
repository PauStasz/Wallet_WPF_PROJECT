using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Helpers
{
    public static class Authentication
    {
        public static string GetHashPassword(string password)
        {
            using var newSha256 = SHA256.Create();
            {
                byte[] shaBytes = newSha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var sb = new StringBuilder();

                foreach (byte b in shaBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static bool VerifyPassword(string password, string hashedPasswordFromDB)
        {
            string passwordToVerify = GetHashPassword(password);

            if (passwordToVerify == hashedPasswordFromDB)
            {
                return true;
            }
            else return false;
        }
    }
}
