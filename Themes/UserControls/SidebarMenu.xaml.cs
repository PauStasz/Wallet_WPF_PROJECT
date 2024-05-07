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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wallet.Views;

namespace Wallet.Themes.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy SidebarMenu.xaml
    /// </summary>
    public partial class SidebarMenu : UserControl
    {
        public SidebarMenu()
        {
            InitializeComponent();
        }

        private void GoHome(object sender, RoutedEventArgs e)
        {
           
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                HomeWindow homeWindow = new HomeWindow();
                currentWindow.Content = homeWindow.Content;
            }
        }

        private void GoExpensePlanning(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                ExpensePlanningWindow window = new ExpensePlanningWindow();
                currentWindow.Content = window.Content;
            }
        }

        private void GoRaport(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                ReportAndStatisticsWindow window = new ReportAndStatisticsWindow();
                currentWindow.Content = window.Content;
            }
        }

        private void GoForecast(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                ForecastWindow window = new ForecastWindow();
                currentWindow.Content = window.Content;
 
            }
        }

        private void GoAccount(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                AccountsWindow window = new AccountsWindow();
                currentWindow.Content = window.Content;

            }
        }

        private void GoCategories(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {
                CategoriesWindow window = new CategoriesWindow();
                currentWindow.Content = window.Content;
            }
        }

        private void GoSettings(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow;

            if (currentWindow is not null)
            {   
                SettingsWindow window = new SettingsWindow();
                currentWindow.Content = window.Content;
            }
        }
    }
}
