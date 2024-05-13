﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Views;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand _submitCommand;
        private ICommand _backCommand;
        private User _user = new User();

        public RegistrationViewModel()
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
            if (!User.IsAlreadyCreated())
            {
                User.Register();

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
