using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Views.LoginRegistrationSystemViews;
using Wallet.Views;
using Wallet.Models;

namespace Wallet.ViewModels
{
    internal class SidebarMenuViewModel: INotifyPropertyChanged
    {
        private User _user;
        private string _name = string.Empty;

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

        private Settings _settingsManger;
        private string _settingsNameBttn;
        private string _logoutNameBttn;
        private string _categoryNameBttn;
        private string _accountNameBttn;
        private string _forecastNameBttn;
        private string _raportsNameBttn;
        private string _expenseNameBttn;
        private string _revenueNameBttn;

        public SidebarMenuViewModel()
        {
            _user = new User();
            _settingsManger = new Settings();
            _user.GetCurrentUser();

            Name = (_user.Name + " " + _user.Surname);
            _logoutCommand = new RelayCommand(execute => Logout());
            _accountCommand = new RelayCommand(execute => GoAccount());
            _homeCommand = new RelayCommand(execute => GoHome());
            _settingsCommand = new RelayCommand(execute => GoSettings());
            _forecastCommand = new RelayCommand(execute => GoForecast());
            _raportCommand = new RelayCommand(execute => GoRaport());
            _categoriesCommand = new RelayCommand(execute => GoCategories());
            _expensePlanningCommand = new RelayCommand(execute => GoExpensePlanning());
            _revenueCommand = new RelayCommand(execute => GoRevenue());

            if (!(_user.HasCustomSettings))
            {
                SetLanguage();//polish
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    SetLanguage();

                }
                else//engslish
                {
                    _settingsNameBttn = "SETTINGS";
                    _logoutNameBttn = "LOGOUT";
                    _categoryNameBttn = "CATEGORIES";
                    _accountNameBttn = "ACCOUNTS";
                    _forecastNameBttn = "FORECAST";
                    _raportsNameBttn = "REPORTS AND STATISTICS";
                    _expenseNameBttn = "EXPENSES";
                    _revenueNameBttn = "REVENUES";
                }
            }
        }

        private void SetLanguage()
        {
            _settingsNameBttn = "USTAWIENIA";
            _logoutNameBttn = "WYLOGUJ SIĘ";
            _categoryNameBttn = "KATEGORIE";
            _accountNameBttn = "KONTA";
            _forecastNameBttn = "PROGNOZA";
            _raportsNameBttn = "RAPORTY I STATYSTKI";
            _expenseNameBttn = "WYDATKI";
            _revenueNameBttn = "WPŁYWY";
        }

        public string RevenueNameBttn
        {
            get { return _revenueNameBttn; }
            set
            {
                if (_revenueNameBttn != value)
                {
                    _revenueNameBttn = value;
                    OnPropertyChanged(nameof(RevenueNameBttn));

                }
            }
        }

        public string ExpenseNameBttn
        {
            get { return _expenseNameBttn; }
            set
            {
                if (_expenseNameBttn != value)
                {
                    _expenseNameBttn = value;
                    OnPropertyChanged(nameof(ExpenseNameBttn));

                }
            }
        }

        public string RaportsNameBttn
        {
            get { return _raportsNameBttn; }
            set
            {
                if (_raportsNameBttn != value)
                {
                    _raportsNameBttn = value;
                    OnPropertyChanged(nameof(RaportsNameBttn));

                }
            }
        }
        public string CategoryNameBttn
        {
            get { return _categoryNameBttn; }
            set
            {
                if (_categoryNameBttn != value)
                {
                    _categoryNameBttn = value;
                    OnPropertyChanged(nameof(CategoryNameBttn));

                }
            }
        }

        public string ForecastNameBttn
        {
            get { return _forecastNameBttn; }
            set
            {
                if (_forecastNameBttn != value)
                {
                    _forecastNameBttn = value;
                    OnPropertyChanged(nameof(ForecastNameBttn));

                }
            }
        }

        public string AccountNameBttn
        {
            get { return _accountNameBttn; }
            set
            {
                if (_accountNameBttn != value)
                {
                    _accountNameBttn = value;
                    OnPropertyChanged(nameof(AccountNameBttn));

                }
            }
        }
        public string LogoutNameBttn
        {
            get { return _logoutNameBttn; }
            set
            {
                if (_logoutNameBttn != value)
                {
                    _logoutNameBttn = value;
                    OnPropertyChanged(nameof(LogoutNameBttn));

                }
            }
        }
        public string SettingsNameBttn
        {
            get { return _settingsNameBttn; }
            set
            {
                if (_settingsNameBttn != value)
                {
                    _settingsNameBttn = value;
                    OnPropertyChanged(nameof(SettingsNameBttn));

                }
            }
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

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

    }
}
