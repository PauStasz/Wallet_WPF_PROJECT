using System.ComponentModel;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;
using Wallet.Views.DialogsWindows;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Diagnostics;

namespace Wallet.ViewModels
{
    class AddExpenseViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Expense _expense;
        private User _user;
        private bool _isEditedOrAddedNew;
        private string _name;
        private bool _isValidationEnabledForName = false;
        private double _amount;
        private DateTime _date;
        private Category _categoryManager;
        private ObservableCollection<Category> _category;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                _isValidationEnabledForName = true;

            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                _isValidationEnabledForAmount = true;
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<Category> Categories
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public AddExpenseViewModel()
        {
            _user = new User();
            _expense = new Expense();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

            _categoryManager = new Category();

            Date = DateTime.Now;

            var list = _categoryManager.GetCategoriesById(_user.Id);

            if (list != null && list.Count() > 0)
            {
                Categories = [.. list];
            }
            else
            {
                Categories = new ObservableCollection<Category>();
            }


            
            _saveCommand = new RelayCommand(execute => CreateExpense());
            _cancelCommand = new RelayCommand(execute => CancelAction());
        }

        public AddExpenseViewModel(Expense expense)
        {
            _expense = expense;
            Name = _expense.Name;
            _user = new User();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;

            _categoryManager = new Category();

            Date = DateTime.Now;

            var list = _categoryManager.GetCategoriesById(_user.Id);

            if (list != null && list.Count() > 0)
            {
                Categories = [.. list];
            }
            else
            {
                Categories = new ObservableCollection<Category>();
            }


            _saveCommand = new RelayCommand(execute => CreateExpense());
            _cancelCommand = new RelayCommand(execute => CancelAction());
        }

        public Expense GetExpense()
        {
            return _expense;
        }

        private void CreateExpense()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length <= 0)
            {

                _isValidationEnabledForName = true;
                OnPropertyChanged(nameof(Name));
            }
            else if(Amount <= 0)
            {
                _isValidationEnabledForAmount = true;
                OnPropertyChanged(nameof(Amount));
            }
            else if(SelectedCategory == null)
            {
                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Wybierz kategorię";

                InfoWindow window = new InfoWindow();
                window.Show();
            }
            else if (Internet.IsConnected())
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    _expense.Create(Name, Amount, Date, SelectedCategory, _user.Id);
                    msg.Text = "Wydatek został dodany.";

                    var expense = _expense.GetExpensesByIdUserName(_user.Id, Name);
                }
                else
                {
                    _expense.UpdateOne(Name, Amount, Date, SelectedCategory, _user.Id);
                    msg.Text = "Wydatek został zaktualizowany.";
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow is not null)
                {
                    currentWindow.Close();
                }
            }
            else
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Brak dostępu do internetu";

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow is not null)
                {

                    currentWindow.Close();

                }
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
        }

        private ICommand _cancelCommand;
        private bool _isValidationEnabledForAmount;

        public ICommand CancelCommand
        {
            get
            {

                return _cancelCommand;
            }
        }

        private void CancelAction()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                currentWindow.Close();

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && _isValidationEnabledForName)
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        return "Nie może być puste.";
                    }
                }
                if (columnName == "Amount" && _isValidationEnabledForAmount)
                {
                    if (Amount <= 0)
                    {
                        return "Wydatek nie może być ujemny.";
                    }
                }

                return String.Empty;

            }


        }
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }
    }
}