using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.ComponentModel;
using Wallet.Models.Users;
using System;
using System.IO;
using System.Diagnostics;

namespace Wallet.Models
{
    public class Expense : BaseEntity, INotifyPropertyChanged
    {
        private string _name;
        private double _amount;
        private DateTime _date;
        public int IdCategory { get; set; }

        public int IdAccount { get; set; }
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

        public double Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public int IdUser { get; set; }

        private IGenericRepository<Expense> _repository = new GenericRepository<Expense>();

        private Account _account = new Account();
        public void Create(string name, double amount, DateTime date, Category category, int idUser)
        {

            _account.GetMainActive(idUser);
            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = category.Id;
            IdUser = idUser;
            IdAccount = _account.Id;

            _repository.SetData("expenses", this);
        }

        public List<Expense> GetExpensesById(int idUser)
        {
            List<Expense> expenses = _repository.GetAllData("expenses");
            
            _account.GetMainActive(idUser);

            if (expenses != null)
            {
                var list =  expenses.Where(e => e.IdAccount == _account.Id).ToList();
                
                return list;

            }

            return new List<Expense>();
        }


        public Expense GetExpensesByIdUserName(int idUser, string name)
        {

            List<Expense> expense = _repository.GetAllData("expenses");

            if (expense is not null)
            {
                return expense.FirstOrDefault(a => a.IdUser == idUser && a.Name == name);
            }

            return new Expense();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void UpdateOne(string name, double amount, DateTime date, Category category, int id)
        {
            _account.GetMainActive(id);
            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = category.Id;
            IdUser = id;
            IdAccount = _account.Id;

            _repository.UpdateData("expenses", this);
        }

        internal void Delete(int id)
        {
            _repository.DeleteData("expenses", id);
        }


    }
}

