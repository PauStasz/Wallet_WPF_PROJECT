using MVVMEssentials.ViewModels;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Views;
using Wallet.Views.DialogsWindows;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private User _user = new User();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand _loginCommand;
        private ICommand _createAccountCommand;

        public LoginViewModel() 
        {
            
        }
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
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
            if (User.Authenticate())
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
                //InfoWindow window = new InfoWindow();
                //window.Show();
                MessageBox.Show("Nieprawidłowe dane.");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
