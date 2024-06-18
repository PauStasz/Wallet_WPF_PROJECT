using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.Diagnostics;
using System.Windows.Input;
using Wallet.Helpers;

namespace Wallet.ViewModels
{
    internal class HomeViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Account _account;
        private ExpenseRevenue _expense;
        private Category _category;

        private ICommand _expanses;
        private ICommand _futureExpanses;
        private ICommand _revenues;
        private ICommand _searchCommand;
        private ObservableCollection<ExpenseRevenue> _rawData;
        internal HomeViewModel()
        {
            _user = new User();
            _account = new Account();
            _expense = new ExpenseRevenue();
            _category = new Category();

            _user.GetCurrentUser();
            _account.GetMainActive(_user.Id);


            _expanses = new RelayCommand(execute => SetListToExpanses());
            _futureExpanses = new RelayCommand(execute => SetListToFutureExpanses());
            _revenues = new RelayCommand(execute => SetListToRevenues());
            _searchCommand = new RelayCommand(execute => SearchInItems());

            SetListToExpanses();

        }

        private void SearchInItems()
        {
            if (!string.IsNullOrEmpty(Search))
            {
                var list = _rawData.Where(i => i.Name.ToLower().Contains(Search.ToLower())).ToList();
                Items = [.. list];
            }
            else
            {
                Items = _rawData;
            }

        }

        public bool IsSelected2ForData
        {
            get { return _isSelected2ForData; }
            set
            {
                if (_isSelected2ForData != value)
                {
                    _isSelected2ForData = value;
                    OnPropertyChanged(nameof(IsSelected2ForData));
                    if (value)
                    {
                        IsSelected1ForData = false;
                        IsSelected1ForSalary = false;
                        IsSelected2ForSalary = false;
                        SortByDataDESC();
                    }
                }
            }
        }

        private void SortByDataDESC()
        {
            var toSort = _rawData.ToList();

            toSort.Sort((t1, t2) => t1.Date > t2.Date ? -1 : (t1.Date == t2.Date ? 0 : 1));

            Items = [.. toSort];
        }

        public bool IsSelected1ForData
        {
            get { return _isSelected1ForData; }
            set
            {
                if (_isSelected2ForData != value)
                {
                    _isSelected1ForData = value;
                    OnPropertyChanged(nameof(IsSelected1ForData));
                    if (value)
                    {
                        IsSelected2ForData = false;
                        IsSelected1ForSalary = false;
                        IsSelected2ForSalary = false;
                        SortByDataASC();
                    }
                }
            }
        }

        private void SortByDataASC()
        {
            var toSort = _rawData.ToList();

            toSort.Sort((t1, t2) => t1.Date < t2.Date ? -1 : (t1.Date == t2.Date ? 0 : 1));

            Items = [.. toSort];
        }

        public bool IsSelected1ForSalary
        {
            get { return _isSelected1ForSalary; }
            set
            {
                if (_isSelected1ForSalary != value)
                {
                    _isSelected1ForSalary = value;

                    if (value)
                    {
                        IsSelected2ForSalary = false;
                        IsSelected2ForData = false;
                        IsSelected1ForData = false;
                        SortBySalaryASC();
                    }

                    OnPropertyChanged(nameof(IsSelected1ForSalary));
                }


            }
        }

        public bool IsSelected2ForSalary
        {
            get { return _isSelected2ForSalary; }
            set
            {
                if (_isSelected2ForSalary != value)
                {
                    _isSelected2ForSalary = value;

                    if (value)
                    {
                        IsSelected1ForSalary = false;
                        IsSelected2ForData = false;
                        IsSelected1ForData = false;
                        SortBySalaryDESC();
                    }

                    OnPropertyChanged(nameof(IsSelected2ForSalary));
                }
            }
        }


        private void SortBySalaryASC()
        {
            var toSort = _rawData.ToList();

            toSort.Sort((t1, t2) => t1.Amount < t2.Amount ? -1 : (t1.Amount == t2.Amount ? 0 : 1));

            foreach (var item in toSort)
            {
                Debug.WriteLine(item.Name + " " + item.Amount);
            }

            Items = [.. toSort];

        }

        private void SortBySalaryDESC()
        {

            var toSort = _rawData.ToList();

            toSort.Sort((t1, t2) => t1.Amount > t2.Amount ? -1 : (t1.Amount == t2.Amount ? 0 : 1));

            foreach (var item in toSort)
            {
                Debug.WriteLine(item.Name + " " + item.Amount);
            }

            Items = [.. toSort];

        }
        private void SetListToRevenues()
        {
            List<ExpenseRevenue> data = _expense.GetAllRevenuesByAccountId(_account.Id);


            if (data != null && data.Count() > 0)
            {

                ObservableCollection<ExpenseRevenue> temp = [.. data];
                Items = temp;

            }
            else
            {
                Items = new ObservableCollection<ExpenseRevenue>();
            }

            _rawData = Items;
        }

        private void SetListToFutureExpanses()
        {
            List<ExpenseRevenue> data = _expense.GetAllExpensesByAccountId(_account.Id).Where(d => d.Date > DateTime.Now).ToList();


            if (data != null && data.Count() > 0)
            {

                ObservableCollection<ExpenseRevenue> temp = [.. data];
                Items = temp;

            }
            else
            {
                Items = new ObservableCollection<ExpenseRevenue>();
            }

            _rawData = Items;
        }


        private void SetListToExpanses()
        {

            List<ExpenseRevenue> data = _expense.GetAllExpensesByAccountId(_account.Id).Where(d => d.Date <= DateTime.Now).ToList();

            var getDistinct = data.GroupBy(b => b.Category.Name, StringComparer.OrdinalIgnoreCase).Select(g => g.First()).ToList();

            foreach (var item in data)
            {
                item.DistinctCategory = getDistinct.Any(t => t.Id == item.Id) ? true : false;


            }

            if (data != null && data.Count() > 0)
            {

                ObservableCollection<ExpenseRevenue> temp = [.. data];
                Items = temp;

            }
            else
            {
                Items = new ObservableCollection<ExpenseRevenue>();
            }


            _rawData = Items;
        }

        public ICommand ExpansesCommand
        {
            get
            {
                return _expanses;
            }
        }

        public ICommand FutureExpansesCommand
        {
            get
            {
                return _futureExpanses;
            }
        }

        public ICommand RevenuesCommand
        {
            get
            {
                return _revenues;
            }
        }
        public Account Account { get { return _account; } }

        private ObservableCollection<ExpenseRevenue> _items;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ExpenseRevenue> Items
        {
            get => _items;
            set
            {
                _items = value;

                OnPropertyChanged();
            }
        }

        private ExpenseRevenue selectedItem;

        public ExpenseRevenue SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private string _search;
        private bool _isSelected1ForSalary;
        private bool _isSelected2ForSalary;
        private bool _isSelected1ForData;
        private bool _isSelected2ForData;
        private ObservableCollection<Category> _categories;

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged();
            }
        }


        public ICommand SearchCommand
        {
            get => _searchCommand;
            set
            {
                _searchCommand = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

