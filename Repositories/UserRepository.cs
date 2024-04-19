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
            //enity.Id = przdzielanie id lub w klasie BaseObject 
            //entity.HashPassword = hasowanie hasła
            base.SetData(nameTable, entity);    
        }

    }
}
