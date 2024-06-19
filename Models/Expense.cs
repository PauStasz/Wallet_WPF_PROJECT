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
        public int IdCategory
        {
            get => _idCategory;
            set
            {
                if (_idCategory != value)
                {
                    _idCategory = value;
                    OnPropertyChanged(nameof(IdCategory));
                }
            }
        }

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
        public int IdUser { get; set; }

        private IGenericRepository<Expense> _repository = new GenericRepository<Expense>();
        private IGenericRepository<Category> _categoryRepository = new GenericRepository<Category>();
        private IGenericRepository<Account> _accountRepository = new GenericRepository<Account>();

        private Account _account = new Account();
        private Category _category;
        private int _idCategory;
        private string _deleteTitle;
        private string _editTitle;

        private Settings _settingsManger;
        private User _user;

        public Expense()
        {
            _user = new User();
            _settingsManger = new Settings();
            _user.GetCurrentUser();

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }
        }

        public string DeleteTitle
        {
            get { return _deleteTitle; }
            set
            {
                if (_deleteTitle != value)
                {
                    _deleteTitle = value;
                    OnPropertyChanged(nameof(DeleteTitle));

                }
            }
        }

        public string EditTitle
        {
            get { return _editTitle; }
            set
            {
                if (_editTitle != value)
                {
                    _editTitle = value;
                    OnPropertyChanged(nameof(EditTitle));

                }
            }
        }


        public void Create(string name, double amount, DateTime date, Category category, int idUser)
        {

            _account.GetMainActive(idUser);
            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = category.Id;
            IdUser = idUser;
            IdAccount = _account.Id;

            _account.Salary = -_amount;

            _deleteTitle = null;
            _editTitle = null;

            _repository.SetData("expenses", this);
            _accountRepository.UpdateData("accounts", _account);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }
        }

        public List<Expense> GetExpensesById(int idUser)
        {
            List<Expense> expenses = _repository.GetAllData("expenses");
            List<Category> categories = _categoryRepository.GetAllData("categories");
            List<Expense> list = new List<Expense>();

            _account.GetMainActive(idUser);

            if (expenses != null)
            {
                foreach (Expense expense in expenses)
                {
                    if (expense.IdAccount == _account.Id)
                    {
                        expense.Category = categories.FirstOrDefault(c => c.Id == expense.IdCategory);

                        list.Add(expense);
                    }

                }
                
                
                return list;

            }

            return new List<Expense>();
        }


        public Expense GetExpensesByIdUserName(int idUser, string name)
        {

            List<Expense> expenses = _repository.GetAllData("expenses");
            List<Category> categories = _categoryRepository.GetAllData("categories");

            if (expenses != null)
            {
                foreach (Expense expense in expenses)
                {
                    if (expense.IdAccount == _account.Id)
                    {
                        expense.Category = categories.FirstOrDefault(c => c.Id == expense.IdCategory && c.IdUser == idUser && name == c.Name); 
                        
                        return expense;
                    }
                }
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
            _account.Salary += amount;

            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = category.Id;
            IdUser = id;
            IdAccount = _account.Id;

            _account.Salary = -_amount;

            _editTitle = null;
            _deleteTitle = null;

            _repository.UpdateData("expenses", this);
            _accountRepository.UpdateData("accounts", _account);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }
        }

        internal void Delete(int id)
        {
            _repository.DeleteData("expenses", id);
        }


    }
}

