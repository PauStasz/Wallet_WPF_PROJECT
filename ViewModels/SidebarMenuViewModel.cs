using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Views;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class SidebarMenuViewModel
    {
        public ICommand _logoutCommand;

        public SidebarMenuViewModel()
        {

        }

        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new RelayCommand(execute => Logout());
                }

                return _logoutCommand;
            }
        }

        private void Logout()
        {
            //TOdO CLEAR USER DATA
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                LoginWindow window = new LoginWindow();

                currentWindow.Close();
                window.Show();
            }
        }
    }
}
