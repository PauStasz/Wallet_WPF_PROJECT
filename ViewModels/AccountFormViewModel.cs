using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;

namespace Wallet.ViewModels
{
    public class AccountFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private User _user;
        private Account _account;
        private bool _isEditedOrAddedNew;
        public AccountFormViewModel()
        {
            _user = new User();
            _account = new Account();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;
        }

        public Account GetAccount() => _account;

        public AccountFormViewModel(Account account)
        {
            _user = new User();
            _account = account;
            _name = account.Name;
            _salary = account.Salary;
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;
        }

        private string _name;
        private bool _isValidationEnabledForName = false;
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

        private double _salary;
        private bool _isValidationEnabledForSalary = false;
        public double Salary
        {
            get { return _salary; }
            set
            {
                _salary = value;
                OnPropertyChanged(nameof(Salary));
                _isValidationEnabledForSalary = true;
            }
        }

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(execute => CancelAction());
                }

                return _cancelCommand;
            }
        }

        private void CancelAction()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                currentWindow.Close();

            }
        }

        private ICommand _saveCommand;
        

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(execute => CreateAccount());
                }

                return _saveCommand;
            }
        }

        public string Error
        {

            get { throw new NotImplementedException(); }

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
                if (columnName == "Salary" && _isValidationEnabledForSalary)
                {
                    if (Salary <= 0)
                    {
                        return "Wartość musi być większa od 0";
                    }
                }

                return "";
            }
        }

        private void CreateAccount()
        {
            if (string.IsNullOrEmpty(Name) || Salary <= 0)
            {
                if (string.IsNullOrEmpty(Name))
                {
                    _isValidationEnabledForName = true;
                    OnPropertyChanged(nameof(Name));
                }
                if(Salary <= 0)
                {
                    _isValidationEnabledForSalary = true;
                    OnPropertyChanged(nameof(Salary));
                }

            }
            else if(Internet.IsConnected())
            {
                

                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    _account.Create(Name, Salary, _user.Id);
                    msg.Text = "Konto zostało dodane.";
                }
                else
                {
                    _account.UpdateOne(Name, Salary, _user.Id);
                    msg.Text = "Konto zostało edytowane.";
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow != null)
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

                if (currentWindow != null)
                {

                    currentWindow.Close();

                }
            }

        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
