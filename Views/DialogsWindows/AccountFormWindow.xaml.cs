using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wallet.Models;
using Wallet.ViewModels;

namespace Wallet.Views.DialogsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy AccountFormWindow.xaml
    /// </summary>
    public partial class AccountFormWindow : Window
    {
        AccountFormViewModel _viewModel;
        public AccountFormWindow()
        {
            InitializeComponent();
            _viewModel = new AccountFormViewModel();
            DataContext = _viewModel;
        }

        public AccountFormWindow(Account account)
        {
            InitializeComponent();
            _viewModel = new AccountFormViewModel(account);
            DataContext = _viewModel;
        }

        public Account GetAccount()
        {
            return _viewModel.GetAccount();

        }

        public void DialogIsActive(Action<bool> callback)
        {

            callback(true);
        }
    }

}
