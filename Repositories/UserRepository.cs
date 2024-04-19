using FireSharp;
using Wallet.Database.FirebaseRealTimeDatabase;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;

namespace Wallet.Repositories
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository()  : base()
        {
        }

    }
}
