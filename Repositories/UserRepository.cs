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

        public void DeleteData(string nameTable, User entity)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllData(string nameTable)
        {
            throw new NotImplementedException();
        }

        public User GetData(string nameTable, int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateData(string nameTable, User entity)
        {
            throw new NotImplementedException();
        }
    }
}
