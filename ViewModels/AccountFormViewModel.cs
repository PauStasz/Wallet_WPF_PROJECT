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
using System.Windows.Interop;
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
        private Settings _settingsManager;
        public AccountFormViewModel()
        {
            _user = new User();
            _account = new Account();
            _settingsManager = new Settings();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

            _addIconCommand = new RelayCommand(execute => AddIcon());

            if (!(_user.HasCustomSettings))
            {

                SetLanguage();
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language)
                {

                    SetLanguage();

                }
                else
                {
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";

                }
            }
        }

        public string NameLabel
        {
            get => _NameLabel;
            set
            {
                _NameLabel = value;
                OnPropertyChanged(nameof(NameLabel));
            }
        }

        public string SaveLabel
        {
            get => _SaveLabel;
            set
            {
                _SaveLabel = value;
                OnPropertyChanged(nameof(SaveLabel));
            }
        }

        public string CancelLabel
        {
            get => _CancelLabel;
            set
            {
                _CancelLabel = value;
                OnPropertyChanged(nameof(CancelLabel));
            }
        }
        private void SetLanguage()
        {
            Name = "Nazwa";
            SaveLabel = "ZAPISZ";
            CancelLabel = "ANULUJ";
        }

        private void AddIcon()
        {
            throw new NotImplementedException();
        }

        public Account GetAccount() => _account;

        public AccountFormViewModel(Account account)
        {
            _user = new User();
            _settingsManager = new Settings();
            _account = account;
            _name = account.Name;
            _salary = account.Salary;
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;

            _addIconCommand = new RelayCommand(execute => AddIcon());

            if (!(_user.HasCustomSettings))
            {

                SetLanguage();
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language)
                {

                    SetLanguage();

                }
                else
                {
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";

                }
            }
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


        private ICommand _addIconCommand;

        public ICommand AddIconCommand
        {
            get
            {

                return _addIconCommand;
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
        private string _NameLabel;
        private string _SaveLabel;
        private string _CancelLabel;

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

            get
            {
                return string.Empty;
            }

        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && _isValidationEnabledForName)
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Nie może być puste.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Nie może być puste.";

                            }
                            else
                            {
                                temp = "It can't be empty.";

                            }
                        }

                        return temp;
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Name, @"[A-Z][a-z]+"))
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Imię powinien składać się z liter, w tym jednej dużej na początku.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Imię powinien składać się z liter, w tym jednej dużej na początku.";

                            }
                            else
                            {
                                temp = "The name should consist of letters, including one capital letter at the beginning.";

                            }
                        }

                        return temp;
                        
                    }
                    else if (Name.Length > 20)
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Nie może przekraczać 20 znaków.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Nie może przekraczać 20 znaków.";

                            }
                            else
                            {
                                temp = "Cannot exceed 20 characters.";

                            }
                        }

                        return temp;
                    }
                }
                if (columnName == "Salary" && _isValidationEnabledForSalary)
                {
                    if (Salary <= 0)
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Wartość musi być większa od 0";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Wartość musi być większa od 0";

                            }
                            else
                            {
                                temp = "The value must be greater than 0";

                            }
                        }

                        return temp;
                        
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

                    if (!(_user.HasCustomSettings))
                    {

                        msg.Text = "Konto zostało dodane.";
                    }
                    else
                    {
                        _settingsManager.GetSettings(_user.Id);

                        if (_settingsManager.Language)
                        {

                            msg.Text = "Konto zostało dodane.";

                        }
                        else
                        {
                            msg.Text = "The account has been added.";

                        }
                    }
                    
                }
                else
                {
                    _account.UpdateOne(Name, Salary, _user.Id);

                    if (!(_user.HasCustomSettings))
                    {

                        msg.Text = "Konto zostało edytowane.";
                    }
                    else
                    {
                        _settingsManager.GetSettings(_user.Id);

                        if (_settingsManager.Language)
                        {

                            msg.Text = "Konto zostało edytowane.";

                        }
                        else
                        {
                            msg.Text = "The account has been edited.";

                        }
                    }
                    
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

                _account.UpdateOne(Name, Salary, _user.Id);

                if (!(_user.HasCustomSettings))
                {

                    msg.Text = "Brak dostępu do internetu";
                }
                else
                {
                    _settingsManager.GetSettings(_user.Id);

                    if (_settingsManager.Language)
                    {

                        msg.Text = "Brak dostępu do internetu";

                    }
                    else
                    {
                        msg.Text = "No internet access.";

                    }
                }
                

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
