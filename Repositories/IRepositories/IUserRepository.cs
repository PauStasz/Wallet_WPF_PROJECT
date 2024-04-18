using Wallet.Models.Users;

namespace Wallet.Repositories.IRepositories
{
    internal interface IUserRepository
    {
        public List<User> GetAllData(string nameTable);
        public User GetData(string nameTable, int id);
        public void SetData(string nameTable, User entity);
        public void UpdateData(string nameTable, User entity);
        public void DeleteData(string nameTable, User entity);
    }
}
