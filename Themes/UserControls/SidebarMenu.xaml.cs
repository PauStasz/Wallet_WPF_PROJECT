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
           
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                HomeWindow window = new HomeWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoExpensePlanning(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                ExpensePlanningWindow window = new ExpensePlanningWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoRaport(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                ReportAndStatisticsWindow window = new ReportAndStatisticsWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoForecast(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                ForecastWindow window = new ForecastWindow();
                currentWindow.Close();
                window.Show();

            }
        }

        private void GoAccount(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                AccountsWindow window = new AccountsWindow();
                currentWindow.Close();
                window.Show();

            }
        }

        private void GoCategories(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {
                CategoriesWindow window = new CategoriesWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoSettings(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow is not null)
            {   
                SettingsWindow window = new SettingsWindow();
                currentWindow.Close();
                window.Show();
            }
        }
    }
}
