using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
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
    class CategoryFormViewModel : INotifyPropertyChanged
    {
        private Category _category;
        private User _user;
        private bool _isEditedOrAddedNew;


        public CategoryFormViewModel()
        {
            _user = new User();
            _category = new Category();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());

        }

        public CategoryFormViewModel(Category category)
        {
            _category = category;
            Name = _category.Name;
            _user = new User();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());
        }

        private string _name;
        private bool _isValidationEnabledForName = false;

        public event PropertyChangedEventHandler? PropertyChanged;

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
        public Category GetCategory()
        {
            return _category;
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
        }

        private void CreateCategory()
        {
            if (string.IsNullOrEmpty(Name))
            {
                if (string.IsNullOrEmpty(Name))
                {
                    _isValidationEnabledForName = true;
                    OnPropertyChanged(nameof(Name));
                }

            }
            else if (Internet.IsConnected())
            {


                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    _category.Create(Name, _user.Id);
                    msg.Text = "Kategoria została dodana.";
                }
                else
                {
                    _category.UpdateOne(Name, _user.Id);
                    msg.Text = "Kategoria została edytowana.";
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

                return "";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
