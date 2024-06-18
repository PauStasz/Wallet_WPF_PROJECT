using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.Diagnostics;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wallet.Models
{
    internal class ExpenseRevenue : BaseEntity
    {
        public int IdUser { get; set; }
        public int IdAccount { get; set; }

        public int IdCategory { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public double Amount { get; set; }

        public Category Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public bool DistinctCategory
        {
            get => _distinctcategory;
            set
            {
                if (_distinctcategory != value)
                {
                    _distinctcategory = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSelectedCategory
        {
            get => _isSelectedCategory;
            set
            {
                if (_isSelectedCategory != value)
                {

                    _isSelectedCategory = value;
                    OnPropertyChanged();

                }
            }
        }


        private IGenericRepository<ExpenseRevenue> _repository = new GenericRepository<ExpenseRevenue>();
        private IGenericRepository<Category> _categoryRepository = new GenericRepository<Category>();
        private IGenericRepository<Account> _accountRepository = new GenericRepository<Account>();
        private Category _category;

        private bool _isSelectedCategory;
        private bool _distinctcategory;

        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        internal List<ExpenseRevenue> GetAllExpensesByAccountId(int id)
        {
            List<ExpenseRevenue> expenses = _repository.GetAllData("expenses");
            List<Category> categories = _categoryRepository.GetAllData("categories");
            List<ExpenseRevenue> result = new List<ExpenseRevenue>();

            if (expenses != null)
            {
                foreach (ExpenseRevenue expense in expenses)
                {
                    if (expense.IdAccount == id)
                    {
                        expense.Category = categories.FirstOrDefault(c => c.Id == expense.IdCategory);

                        result.Add(expense);
                    }

                }
            }


            return result;
        }

        public ExpenseRevenue()
        {
            _isSelectedCategory = true;
        }

        private Account _account = new Account();

        internal List<ExpenseRevenue> GetAllRevenuesByAccountId(int id)
        {
            List<ExpenseRevenue> revenues = _repository.GetAllData("revenues");
            List<Category> categories = _categoryRepository.GetAllData("categories");
            List<ExpenseRevenue> result = new List<ExpenseRevenue>();

            if (revenues != null)
            {
                foreach (ExpenseRevenue revenue in revenues)
                {
                    if (revenue.IdAccount == id)
                    {
                        revenue.Category = categories.FirstOrDefault(c => c.Id == revenue.IdCategory);

                        result.Add(revenue);
                    }

                }
            }

            return result;
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void DeleteRevenue(int id)
        {
            _repository.DeleteData("revenues", id);
        }

        internal void Create(string name, double amount, DateTime date, Category selectedCategory, int id)
        {
            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = selectedCategory.Id;
            IdUser = id;
             _account.GetMainActive(id);
            IdAccount = _account.Id;

            _account.Salary += Amount;

            _accountRepository.UpdateData("accounts", _account);
            _repository.SetData("revenues", this);
        }

        internal void UpdateOne(string name, double amount, DateTime date, Category selectedCategory, int id)
        {
            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = selectedCategory.Id;
            IdUser = id;
            _account.GetMainActive(id);
            IdAccount = _account.Id;

            _account.Salary += Amount;

            _accountRepository.UpdateData("accounts", _account);
            _repository.SetData("revenues", this);
        }
    }
}
