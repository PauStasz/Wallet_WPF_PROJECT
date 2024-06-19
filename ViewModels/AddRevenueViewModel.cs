using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models.Users;
using Wallet.Models;
using Wallet.Helpers;
using System.Windows.Input;
using System.Windows;
using Wallet.Views.DialogsWindows;
using System.Diagnostics;

namespace Wallet.ViewModels
{
    internal class AddRevenueViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Settings _settingsManager;
        public AddRevenueViewModel()
        {
            _user = new User();
            _revenue = new ExpenseRevenue();
            _settingsManager = new Settings();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

            _categoryManager = new Category();

            Date = DateTime.Now;

            var list = _categoryManager.GetCategoriesById(_user.Id);

            if (list != null && list.Count() > 0)
            {
                Categories = [.. list];
            }
            else
            {
                Categories = new ObservableCollection<Category>();
            }



            _saveCommand = new RelayCommand(execute => CreateExpense());
            _cancelCommand = new RelayCommand(execute => CancelAction());

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
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";
                    AmountLabel = "Amount";
                    CategoryLabel = "Categories";

                }
            }
        }

        public string NameLabel
        {
            get => _NameLabel;
            set
            {
                _NameLabel = value;
                OnPropertyChanged(nameof(NameLabel));
            }
        }

        public string SaveLabel
        {
            get => _SaveLabel;
            set
            {
                _SaveLabel = value;
                OnPropertyChanged(nameof(SaveLabel));
            }
        }

        public string CancelLabel
        {
            get => _CancelLabel;
            set
            {
                _CancelLabel = value;
                OnPropertyChanged(nameof(CancelLabel));
            }
        }

        public string CategoryLabel
        {
            get => _CategoryLabel;
            set
            {
                _CategoryLabel = value;
                OnPropertyChanged(nameof(CategoryLabel));
            }
        }
        public string AmountLabel
        {
            get => _AmountLabel;
            set
            {
                _AmountLabel = value;
                OnPropertyChanged(nameof(AmountLabel));
            }
        }
        private void SetLanguage()
        {
            Name = "Nazwa";
            SaveLabel = "ZAPISZ";
            CancelLabel = "ANULUJ";
            AmountLabel = "Kwota";
            CategoryLabel = "Kategorie";
        }
        public AddRevenueViewModel(ExpenseRevenue revenue)
        {
            _revenue = revenue;
            Name = _revenue.Name;
            _user = new User();
            _settingsManager = new Settings();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;
            Amount = _revenue.Amount;
            Date = _revenue.Date;

            _categoryManager = new Category();

            var list = _categoryManager.GetCategoriesById(_user.Id);

            if (list != null && list.Count() > 0)
            {
                Categories = [.. list];
            }
            else
            {
                Categories = new ObservableCollection<Category>();
            }


            _saveCommand = new RelayCommand(execute => CreateExpense());
            _cancelCommand = new RelayCommand(execute => CancelAction());


            
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
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";
                    AmountLabel = "Amount";
                    CategoryLabel = "Categories";

                }
            }

        }


        private ExpenseRevenue _revenue;
        private User _user;
        private bool _isEditedOrAddedNew;
        private string _name;
        private bool _isValidationEnabledForName = false;
        private double _amount;
        private DateTime _date;
        private Category _categoryManager;
        private ObservableCollection<Category> _category;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                _isValidationEnabledForName = true;

            }
        }

        public double Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                _isValidationEnabledForAmount = true;
            }
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        private Category _selectedCategory;
        private bool _isValidationEnabledForAmount = false;

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public ObservableCollection<Category> Categories
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Categories));
            }
        }



        public ExpenseRevenue GetRevenue()
        {
            return _revenue;
        }

        private void CreateExpense()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length <= 0)
            {

                _isValidationEnabledForName = true;
                OnPropertyChanged(nameof(Name));
            }
            else if (Amount <= 0)
            {
                _isValidationEnabledForAmount = true;
                OnPropertyChanged(nameof(Amount));
            }
            else if (SelectedCategory == null)
            {
                MessageHolder msg = MessageHolder.Instance;
                if (!(_user.HasCustomSettings))
                {

                    msg.Text = "Wybierz kategorię";
                }
                else
                {
                    _settingsManager.GetSettings(_user.Id);

                    if (_settingsManager.Language)
                    {

                        msg.Text = "Wybierz kategorię";

                    }
                    else
                    {
                        msg.Text = "Select a category";

                    }
                }

                InfoWindow window = new InfoWindow();
                window.Show();
            }
            else if (Internet.IsConnected())
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    
                    _revenue.Create(Name, Amount, Date, SelectedCategory, _user.Id);
                    if (!(_user.HasCustomSettings))
                    {

                        msg.Text = "Wpływ został dodany.";
                    }
                    else
                    {
                        _settingsManager.GetSettings(_user.Id);

                        if (_settingsManager.Language)
                        {

                            msg.Text = "Wpływ został dodany.";

                        }
                        else
                        {
                            msg.Text = "The revenue has been added.";

                        }
                    }
                }
                else
                {
                    _revenue.UpdateOne(Name, Amount, Date, SelectedCategory, _user.Id, _revenue.Id);
                    if (!(_user.HasCustomSettings))
                    {

                        msg.Text = "Wpływ został edytowany.";
                    }
                    else
                    {
                        _settingsManager.GetSettings(_user.Id);

                        if (_settingsManager.Language)
                        {

                            msg.Text = "Wpływ został edytowany.";

                        }
                        else
                        {
                            msg.Text = "The revenue has been edited.";

                        }
                    }
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow != null)
                {
                    currentWindow.Close();
                }
            }
            else
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;
                if (!(_user.HasCustomSettings))
                {

                    msg.Text = "Brak dostępu do internetu";
                }
                else
                {
                    _settingsManager.GetSettings(_user.Id);

                    if (_settingsManager.Language)
                    {

                        msg.Text = "Brak dostępu do internetu";

                    }
                    else
                    {
                        msg.Text = "No internet access.";

                    }
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow is not null)
                {

                    currentWindow.Close();

                }
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
        }

        private ICommand _cancelCommand;
        private string _SaveLabel;
        private string _AmountLabel;
        private string _CategoryLabel;
        private string _CancelLabel;
        private string _NameLabel;

        public ICommand CancelCommand
        {
            get
            {

                return _cancelCommand;
            }
        }

        private void CancelAction()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                currentWindow.Close();

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && _isValidationEnabledForName)
                {
                    if (string.IsNullOrEmpty(Name))
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Nie może być puste.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Nie może być puste.";

                            }
                            else
                            {
                                temp = "It can't be empty.";

                            }
                        }

                        return temp;
                    }
                }
                if (columnName == "Amount" && _isValidationEnabledForAmount)
                {
                    if (Amount <= 0)
                    {
                         string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Wartość musi być większa od 0";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Wartość musi być większa od 0";

                            }
                            else
                            {
                                temp = "The value must be greater than 0";

                            }
                        }

                        return temp;
                    }
                }

                return String.Empty;

            }


        }
        public string Error
        {
            get
            {
                return string.Empty;
            }
        }

    }
}
