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
    public class RegistrationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand _submitCommand;
        private ICommand _backCommand;
        private User _user = new User();

        private string _email;
        private string _password;
        private string _name;
        private string _surname;
        private string _nick;
        private string _confirmationPassword;

        private bool _isValidationEnabledForEmail = false;
        private bool _isValidationEnabledForPassword = false;
        private bool _isValidationEnabledForName = false;
        private bool _isValidationEnabledForSurname = false;
        private bool _isValidationEnabledForNick = false;
        private bool _isValidationEnabledForConfirmationPassword = false;
        public RegistrationViewModel()
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

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
                _isValidationEnabledForSurname = true;
            }
        }

        public string ConfirmationPassword
        {
            get { return _confirmationPassword; }
            set
            {
                _confirmationPassword = value;
                OnPropertyChanged(nameof(ConfirmationPassword));
                _isValidationEnabledForConfirmationPassword = true;
            }
        }

        public string Nick
        {
            get { return _nick; }
            set
            {
                _nick = value;
                OnPropertyChanged(nameof(Nick));
                _isValidationEnabledForNick = true;
            }
        }

        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new RelayCommand(execute => CreateAccount());
                }

                return _submitCommand;
            }
        }

        public ICommand BackCommand
        {
            get
            {
                if (_backCommand == null)
                {
                    _backCommand = new RelayCommand(execute => BackToLoginWindow());
                }

                return _backCommand;
            }
        }

        private void BackToLoginWindow()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                LoginWindow window = new LoginWindow();

                currentWindow.Close();
                window.Show();
            }
        }
        private void CreateAccount()
        {
            if (Internet.IsConnected())
            {
                if (IsDataNullOrEmpty())
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
                    if (string.IsNullOrEmpty(Nick))
                    {
                        _isValidationEnabledForNick = true;
                        OnPropertyChanged(nameof(Nick));
                    }
                    if (string.IsNullOrEmpty(ConfirmationPassword))
                    {
                        _isValidationEnabledForConfirmationPassword = true;
                        OnPropertyChanged(nameof(ConfirmationPassword));
                    }
                    if (string.IsNullOrEmpty(Surname))
                    {
                        _isValidationEnabledForSurname = true;
                        OnPropertyChanged(nameof(Surname));
                    }
                    if (string.IsNullOrEmpty(Name))
                    {
                        _isValidationEnabledForName = true;
                        OnPropertyChanged(nameof(Name));
                    }
                }
                else if (!_user.IsAlreadyCreated(_email))
                {
                    _user.Register(_email, _name, _surname, _nick, _password);

                    Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                    if (currentWindow is not null)
                    {
                        HomeWindow window = new HomeWindow();

                        currentWindow.Close();
                        window.Show();

                        MessageHolder msg = MessageHolder.Instance;
                        msg.Text = "Utworzono konto.";
                        InfoWindow infoWindow = new InfoWindow();
                        infoWindow.Show();
                    }
                }
                else
                {
                    MessageHolder msg = MessageHolder.Instance;
                    msg.Text = "Błędne dane.";
                    InfoWindow window = new InfoWindow();
                }
            }
            else
            {
                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Brak dostępu do internetu.";
                InfoWindow window = new InfoWindow();
                window.Show();
            }
        }

        private bool IsDataNullOrEmpty()
        {
            return (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmationPassword) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Nick));
        }

        protected virtual void OnPropertyChanged(string propertyName)
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
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Name, @"[A-Z][a-z]+"))
                    {
                        return "Imię powinien składać się z liter, w tym jednej dużej na początku.";
                    }
                    else if (Name.Length > 20)
                    {
                        return "Nie może przekraczać 20 znaków.";
                    }
                }
                if (columnName == "Surname" && _isValidationEnabledForSurname)
                {
                    if (string.IsNullOrEmpty(Surname))
                    {
                        return "Nie może być puste.";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Surname, @"[A-Z][a-z]+"))
                    {
                        return "Nazwisko powinien składać się z liter, w tym jednej dużej na początku.";
                    }
                    else if(Surname.Length > 20)
                    {
                        return "Nie może przekraczać 20 znaków.";
                    }
                }
                if (columnName == "Nick" && _isValidationEnabledForNick)
                {
                    if (string.IsNullOrEmpty(Nick))
                    {
                        return "Nie może być puste.";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Nick, @"[A-Za-z0-9]+"))
                    {
                        return "Nick powinien składać się z liter i cyfr.";
                    }
                    else if (Nick.Length > 15)
                    {
                        return "Nie może przekraczać 15 znaków.";
                    }
                }
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
                if (columnName == "ConfirmationPassword" && (_isValidationEnabledForPassword || _isValidationEnabledForConfirmationPassword))
                {
                    if (string.IsNullOrEmpty(ConfirmationPassword))
                    {
                        return "Nie może być puste.";
                    }
                    else if (Password != ConfirmationPassword)
                    {
                        return "Hasła nie są takie same.";
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
