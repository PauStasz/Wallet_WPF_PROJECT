﻿using System.Collections.ObjectModel;
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
        private Settings _settingsManager;
        public AccountViewModel()
        {
            _user = new User();
            _account = new Account();
            _user.GetCurrentUser();
            _settingsManager = new Settings();

            _deleteCommand = new RelayCommand(DeleteAccount);
            _addAccountCommand = new RelayCommand(execute => AddAccount());
            _editCommand = new RelayCommand(EditAccount);
            _selectMainAccountCommand = new RelayCommand(SelectMainAccount);

            SetItems();

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
                    TitleWindow = "ACCOUNTS";
                    AddBttnTitle = "ADD NEW ACCOUNT";
                }
            }
        }

        public string AddBttnTitle
        {
            get => _AddBttnTitle;
            set
            {
                _AddBttnTitle = value;
                OnPropertyChanged();
            }
        }
        public string TitleWindow
        {
            get => _TitleWindow;
            set
            {
                _TitleWindow = value;
                OnPropertyChanged();
            }
        }
        private void SetLanguage()
        {
            TitleWindow = "KONTA";
            AddBttnTitle = "DODAJ NOWE KONTO";
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
                Items.Remove(item);
                _account.Delete(item.Id);
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

            ObservableCollection<Account> temp = [.. data];
            _items = temp;
        }

        private Account selectedItem;
        private string _AddBttnTitle;
        private string _TitleWindow;

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
