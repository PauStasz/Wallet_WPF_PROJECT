using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Account _account;
        public event PropertyChangedEventHandler? PropertyChanged;
        public AccountViewModel()
        {
            _user = new User();
            _account = new Account();
            _user.GetCurrentUser();
            SetItems();

        }

        private ICommand _addAccountCommand;
        public ICommand AddAccountCommand
        {
            get
            {
                if (_addAccountCommand == null)
                {
                    _addAccountCommand = new RelayCommand(execute => AddAccount());
                }

                return _addAccountCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(execute => DeleteAccount(execute));
                }

                return _deleteCommand;
            }
        }

        private void DeleteAccount(object parameter)
        {
            MessageBox.Show("Usinueto");
            if (parameter is Account item && Items.Contains(item))
            {
                Items.Remove(item);
                _account.Delete(item.Id);
                //SetItems();
            }
        }

        private ObservableCollection<Account> _items;
        public ObservableCollection<Account> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }


        private void SetItems()
        {
            List<Account> data = _account.GetAccountsById(_user.Id);

            ObservableCollection<Account> temp = [.. data];
            _items = temp;
        }

        private Account selectedItem;
        public Account SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private void AddAccount()
        {
            AccountFormWindow window = new AccountFormWindow();
            window.Show();
            //to do after add SetItems();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
