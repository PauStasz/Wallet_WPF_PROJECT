using System.Text;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;
using System.Security.Cryptography;

namespace Wallet.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository() : base()
        {

        }        

        private int GenerateUserID()
        {
            Guid newGuid = Guid.NewGuid();
            byte[] newGuidByte = newGuid.ToByteArray();
            int intId = BitConverter.ToInt32(newGuidByte, 0);

            return intId;
        }

        public static string HashPassword(string password)
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
            string passwordToVerify = HashPassword(password);
            
            if (passwordToVerify == hashedPasswordFromDB)
            {
                return true;
            }
            else return false;
        }

        public override void SetData(string nameTable, User entity)
        {
            entity.Id = GenerateUserID();
            entity.HashPassword = HashPassword(entity.Password);
            base.SetData(nameTable, entity);    
        }

    }
}
