using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Views;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    public class SidebarMenuViewModel : INotifyPropertyChanged
    {
        
        private User _user;
        private string _name;

        private ICommand _settingsCommand;
        private ICommand _forecastCommand;
        private ICommand _raportCommand;
        private ICommand _homeCommand;
        private ICommand _categoriesCommand;
        private ICommand _expensePlanningCommand;
        private ICommand _logoutCommand;
        private ICommand _accountCommand;
        private ICommand _revenueCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SidebarMenuViewModel()
        {
            _user = new User();
            _user.GetCurrentUser();

            Name = _user.Name.ToString() + " " + _user.Surname.ToString();

            _logoutCommand = new RelayCommand(execute => Logout());
            _accountCommand = new RelayCommand(execute => GoAccount());
            _homeCommand = new RelayCommand(execute => GoHome());
            _settingsCommand = new RelayCommand(execute => GoSettings());
            _forecastCommand = new RelayCommand(execute => GoForecast());
            _raportCommand = new RelayCommand(execute => GoRaport());
            _categoriesCommand = new RelayCommand(execute => GoCategories());
            _expensePlanningCommand = new RelayCommand(execute => GoExpensePlanning());
            _revenueCommand = new RelayCommand(execute => GoRevenue());

        }

        private void GoRevenue()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                Revenue window = new Revenue();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoExpensePlanning()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                ExpensePlanningWindow window = new ExpensePlanningWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoRaport()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                ReportAndStatisticsWindow window = new ReportAndStatisticsWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoForecast()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                ForecastWindow window = new ForecastWindow();
                currentWindow.Close();
                window.Show();

            }
        }

        private void GoCategories()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                CategoriesWindow window = new CategoriesWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoSettings()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                SettingsWindow window = new SettingsWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoHome()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                HomeWindow window = new HomeWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        private void GoAccount()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive); ;

            if (currentWindow != null)
            {
                AccountsWindow window = new AccountsWindow();
                currentWindow.Close();
                window.Show();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand;
            }
        }

        public ICommand AccountCommand
        {
            get
            {
                return _accountCommand;
            }
        }

        public ICommand RevenueCommand
        {
            get
            {
                return _revenueCommand;
            }
        }
        public ICommand SettingsCommand
        {
            get
            {
                return _settingsCommand;
            }
        }

        public ICommand ForecastCommand
        {
            get
            {
                return _forecastCommand;
            }
        }

        public ICommand RaportCommand
        {
            get
            {
                return _raportCommand;
            }
        }

        public ICommand HomeCommand
        {
            get
            {
                return _homeCommand;
            }
        }

        public ICommand CategoriesCommand
        {
            get
            {
                return _categoriesCommand;
            }
        }

        public ICommand ExpensePlanningCommand
        {
            get
            {
                return _expensePlanningCommand;
            }
        }
        private void Logout()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
                LoginWindow window = new LoginWindow();

                currentWindow.Close();
                window.Show();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
