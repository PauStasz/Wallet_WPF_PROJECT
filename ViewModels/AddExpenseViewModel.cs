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

namespace Wallet.ViewModels
{
    class AddExpenseViewModel : INotifyPropertyChanged
    {
        private Expense _expense;
        private User _user;
        private bool _isEditedOrAddedNew;
        private string _name;
        private bool _isValidationEnabledForName = false;
        private decimal _amount;
        private DateTime _date;
        private string _category;

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

        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
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

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }

        public AddExpenseViewModel()
        {
            _user = new User();
            _expense = new Expense();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

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
            else if (Internet.IsConnected())
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    _expense.Create(Name, Amount, Date, Category, _user.Id);
                    msg.Text = "Wydatek został dodany.";

                    var expense = _expense.GetExpensesByIdUserName(_user.Id, Name);
                }
                else
                {
                    _expense.UpdateOne(Name, Amount, Date, Category, _user.Id);
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
    }
}