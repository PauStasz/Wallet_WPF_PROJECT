using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;

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

            _deleteCommand = new RelayCommand(DeleteAccount);
            _addAccountCommand = new RelayCommand(execute => AddAccount());
            _editCommand = new RelayCommand(EditAccount);
            _selectMainAccountCommand = new RelayCommand(SelectMainAccount);

            SetItems();


        }

        private ICommand _selectMainAccountCommand;
        public ICommand SelectMainAccountCommand
        {
            get
            {
                return _selectMainAccountCommand;
            }
        }

        private void SelectMainAccount(object parameter)
        {
            if (parameter is Account item)
            {
                foreach (Account temp in Items)
                {
                    if (item.Id != temp.Id)
                    {
                        temp.IsMain = false;
                        temp.MainAccount = "USTAW JAKO GŁÓWNE";
                    }
                    else
                    {
                        temp.IsMain = true;
                        temp.MainAccount = "WYBRANO NA GŁÓWNE";
                    }
                }

                _account.UpdateAll(Items);
            }
        }

        private ICommand _addAccountCommand;
        public ICommand AddAccountCommand
        {
            get
            {
                return _addAccountCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand 
        {
            get
            {
                return _deleteCommand;
            }
        }

        private void DeleteAccount(object parameter)
        {
            if (parameter is Account item)
            {
                if (!item.IsMain)
                {
                    Items.Remove(item);
                    _account.Delete(item.Id);
                }
                else
                {
                    MessageHolder msg1 = MessageHolder.Instance;
                    msg1.Text = "Ustaw inne konto główne.";

                    InfoWindow window1 = new InfoWindow();
                    window1.Show();
                }
                
            }
        }

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand;
            }
        }

        private void EditAccount(object parameter)
        {
            if (parameter is Account item)
            {
                AccountFormWindow window = new AccountFormWindow(item);
                window.Show();        
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


            if (data != null && data.Count() > 0)
            {
                ObservableCollection<Account> temp = [.. data];
                _items = temp;

            }
            else
            {
                Items = new ObservableCollection<Account>();
            }
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
            OpenAddAccountDialog(UpdateItems);
        }

        private void UpdateItems(Account result)
        {
            if (result != null)
            {
                Items.Add(result);
            }
        }

        private void OpenAddAccountDialog(Action<Account> callback)
        {
            AccountFormWindow window = new AccountFormWindow();

            window.Closed += (sender, args) => callback(window.GetAccount());
            window.Show();
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
