using MVVMEssentials.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views;
using Wallet.Views.DialogsWindows;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private User _user = new User();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand _loginCommand;
        private ICommand _createAccountCommand;



        private string _email = "";
        private string _password = "";
        private bool _isValidationEnabledForEmail = false;
        private bool _isValidationEnabledForPassword = false;

        public LoginViewModel()
        {

        }
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                _isValidationEnabledForEmail = true;
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                _isValidationEnabledForPassword = true;
            }
        }

        public ICommand CreateAccountCommand
        {
            get
            {
                if (_createAccountCommand == null)
                {
                    _createAccountCommand = new RelayCommand(execute => CreateAccount());
                }

                return _createAccountCommand;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(execute => Login());
                }

                return _loginCommand;
            }
        }



        private void CreateAccount()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                RegistrationWindow window = new RegistrationWindow();

                currentWindow.Close();
                window.Show();

            }
        }
        private void Login()
        {
            if (Internet.IsConnected())
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    if (string.IsNullOrEmpty(Email))
                    {
                        _isValidationEnabledForEmail = true;
                        OnPropertyChanged(nameof(Email));

                    }
                    if (string.IsNullOrEmpty(Password))
                    {
                        _isValidationEnabledForPassword = true;
                        OnPropertyChanged(nameof(Password));
                    }
                }
                else if (_user.Authenticate(_email, _password))
                {

                    Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                    if (currentWindow is not null)
                    {
                        HomeWindow window = new HomeWindow();

                        currentWindow.Close();
                        window.Show();
                    }
                }
                else
                {
                    MessageHolder msg = MessageHolder.Instance;
                    msg.Text = "Błędne dane logowania";
                    InfoWindow window = new InfoWindow();
                    window.Show();
                }
            }
            else
            {
                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Brak dostępu do internetu";

                InfoWindow window = new InfoWindow();
                window.Show();
            }

        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Email" && _isValidationEnabledForEmail)
                {
                    if (string.IsNullOrEmpty(Email))
                    {
                        return "Nie może być puste.";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"[a-zA-Z0-9.]+@[a-zA-Z0-9]+.[a-zA-Z0-9]+"))
                    {
                        return "E-mail powinien składać się ze znaków taki jak cyfry, litery, kropki i @.";
                    }
                }
                if (columnName == "Password" && _isValidationEnabledForPassword)
                {
                    if (string.IsNullOrEmpty(Password))
                    {
                        return "Nie może być puste.";
                    }
                    else if (Password.Length < 6 || Password.Length > 20)
                    {
                        return "Hasło musi mieć 6-20 znaków.";
                    }
                }
                return "";

            }


        }
        public string Error

        {

            get { throw new NotImplementedException(); }

        }
    }
}
