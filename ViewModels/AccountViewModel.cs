using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Views.DialogsWindows;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class AccountViewModel
    {
        public ICommand _addAccountCommand;
        public AccountViewModel()
        {
                
        }

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
