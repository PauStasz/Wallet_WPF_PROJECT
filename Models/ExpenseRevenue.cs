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
using Wallet.Models.Users;

namespace Wallet.Models
{
    internal class ExpenseRevenue : BaseEntity, INotifyPropertyChanged
    {
        public int IdUser { get; set; }
        public int IdAccount { get; set; }

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


        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;

                    if (!_user.HasCustomSettings)
                    {
                        DateFormat = _date.ToString("yyyy-dd-MM");
                    }
                    else
                    {
                        _settingsManger.GetSettings(_user.Id);

                        if (_settingsManger.Format) 
                        {

                            DateFormat = _date.ToString("yyyy-dd-MM");

                        }
                        else
                        {
                            DateFormat = _date.ToString("dd-MM-yyyy");
                        }
                    }
                    
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string DateFormat
        {
            get => _dateformat;
            set
            {
                if (_dateformat != value)
                {
                    _dateformat = value;
                    OnPropertyChanged(nameof(Date));
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

        private Settings _settingsManger;
        private User _user;
        public ExpenseRevenue()
        {
            _isSelectedCategory = true;
            _settingsManger = new Settings();
            _user = new User();
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

        private Account _account = new Account();
        private double _amount;
        private string _name;
        private int _idCategory;
        private DateTime _date;
        private string _dateformat;
        private string _deleteTitle;
        private string _editTitle;

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

            _editTitle = null;
            _deleteTitle = null;

            _accountRepository.UpdateData("accounts", _account);
            _repository.SetData("revenues", this);

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

        internal void UpdateOne(string name, double amount, DateTime date, Category selectedCategory, int id, int idData)
        {
            _repository.GetOneData("revenues", idData);
            _account.GetMainActive(id);
            _account.Salary -= Amount;

            Name = name;
            Amount = amount;
            Date = date;
            IdCategory = selectedCategory.Id;
            IdUser = id;
            _account.GetMainActive(id);
            IdAccount = _account.Id;

            
            _account.Salary += Amount;

            _editTitle = null;
            _deleteTitle = null;

            _accountRepository.UpdateData("accounts", _account);
            _repository.SetData("revenues", this);

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
    }
}
