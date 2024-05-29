using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wallet.Models.Users;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;

namespace Wallet.Models
{
    public class Account : BaseEntity, INotifyPropertyChanged
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private double _salary;
        public double Salary
        {
            get => _salary;
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        public int IdUser { get; set; }

        public bool IsMain { get; set; }

        private string _mainAccount;
        public string MainAccount
        {
            get => _mainAccount;
            set
            {
                if (_mainAccount != value)
                {
                    _mainAccount = value;
                    OnPropertyChanged(nameof(MainAccount));
                }
            }
        }

        public Account()
        {
            if (!IsMain)
                _mainAccount = "USTAW JAKO GŁÓWNE";
            else
                _mainAccount = "WYBRANO NA GŁÓWNE";
        }



        private IGenericRepository<Account> _repository = new GenericRepository<Account>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Create(string name, double salary, int idUser)
        {
            Name = name;
            Salary = salary;
            IdUser = idUser;
            IsMain = false;

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

        public void UpdateAll(ObservableCollection<Account> items)
        {

            foreach (Account account in items)
            {
                _repository.UpdateData("accounts", account);
            }
        }

        public void UpdateOne(string name, double salary, int id)
        {

            Name = name;
            Salary = salary;
            IdUser = id;

            _repository.UpdateData("accounts", this);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
