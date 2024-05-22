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



        public override void SetData(string nameTable, User entity)
        {

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
