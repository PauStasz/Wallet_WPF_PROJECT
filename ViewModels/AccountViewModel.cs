using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AccountViewModel
    {
        private User _user;
        private Account _account;
        public AccountViewModel()
        {
            _user = new User();
            _account = new Account();
            _user.GetCurrentUser();
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

        private void AddAccount()
        {
            AccountFormWindow window = new AccountFormWindow();
            window.Show();        
        }
    }
}
