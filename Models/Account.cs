using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;

namespace Wallet.Models
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }

        public double Salary { get; set; }

        public int IdUser { get; set; }

        private IGenericRepository<Account> _repository = new GenericRepository<Account>();

        public void Create(string name, double salary, int idUser)
        {
            Name = name;
            Salary = salary;
            IdUser = idUser;

            _repository.SetData("accounts", this);
        }

        public List<Account> GetAccountsById(int idUser)
        {

            List<Account> accounts = _repository.GetAllData("accounts");

            if (accounts is not null) 
            {
                return accounts.Where(a => a.IdUser == idUser).ToList();
            }

            return new List<Account>();
        }

        public void Delete(int id)
        {
            _repository.DeleteData("accounts", id);
        }
    }
}
