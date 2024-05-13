using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;

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


        public override void SetData(string nameTable, User entity)
        {
            entity.Id = GenerateUserID();

            if (entity.Password is not null && entity.Password.Length > 0)
            {
                entity.HashPassword = Authentication.GetHashPassword(entity.Password);
                entity.Password = "";
                entity.ConfirmPassword = "";
            }

            base.SetData(nameTable, entity);    
        }

    }
}
