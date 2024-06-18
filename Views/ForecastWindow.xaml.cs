

using System.Windows;
using Wallet.ViewModels;

namespace Wallet.Views
{
    public partial class ForecastWindow : Window
    {
        public ForecastWindow()
        {
            InitializeComponent();
            DataContext = new ExpensesViewModel();
        }
    }
}
