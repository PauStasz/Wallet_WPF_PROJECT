using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models.Users;

namespace Wallet.Repositories.IRepositories
{
    internal interface IUserRepository
    {
        public List<User> GetAllData(string nameTable);
        public User GetOneData(string nameTable, int id);
        public void SetData(string nameTable, User entity);
        public void UpdateData(string nameTable, User entity);
        public void DeleteData(string nameTable, int id);
    }
}
