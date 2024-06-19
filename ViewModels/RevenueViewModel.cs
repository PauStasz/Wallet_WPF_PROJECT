using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models.Users;
using Wallet.Models;
using Wallet.Views.DialogsWindows;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;

namespace Wallet.ViewModels
{
    internal class RevenueViewModel : INotifyPropertyChanged
    {
        private User _user;
        private ExpenseRevenue _expenseRevenue;
        private Account _account;
        private Settings _settingsManager;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RevenueViewModel()
        {
            _user = new User();
            _expenseRevenue = new ExpenseRevenue();
            _account = new Account();
            _settingsManager = new Settings();
            _user.GetCurrentUser();
            _account.GetMainActive(_user.Id);

            _addRevenueCommand = new RelayCommand(execute => Add());
            _deleteCommand = new RelayCommand(Delete);
            _editCommand = new RelayCommand(Edit);

            SetItems();


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
                    TitleWindow = "REVENUES";
                    AddBttnTitle = "ADD NEW REVENUE";
                }
            }
        }

        public string AddBttnTitle
        {
            get => _AddBttnTitle;
            set
            {
                _AddBttnTitle = value;
                OnPropertyChanged();
            }
        }
        public string TitleWindow
        {
            get => _TitleWindow;
            set
            {
                _TitleWindow = value;
                OnPropertyChanged();
            }
        }

        private void SetLanguage()
        {
            TitleWindow = "WPŁYWY";
            AddBttnTitle = "DODAJ NOWY WPŁYW";
        }

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand;

        private void Edit(object parameter)
        {
            if (parameter is ExpenseRevenue item)
            {
                AddRevenueWindow window = new AddRevenueWindow
                {
                    DataContext = new AddRevenueViewModel(item)
                };
                window.Show();
            }
        }

        private void SetItems()
        {
            List<ExpenseRevenue> data = _expenseRevenue.GetAllRevenuesByAccountId(_account.Id);


            if (data != null && data.Count() > 0)
            {

                ObservableCollection<ExpenseRevenue> temp = [.. data];
                Items = temp;

            }
            else
            {
                Items = new ObservableCollection<ExpenseRevenue>();
            }
        }

        private ExpenseRevenue _selectedItem;
        public ExpenseRevenue SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ExpenseRevenue> _items;
        public ObservableCollection<ExpenseRevenue> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addRevenueCommand;
        public ICommand AddRevenueCommand => _addRevenueCommand;

        private void Add()
        {
            var window = new AddRevenueWindow();
            window.Closed += (sender, args) => SetItems();
            window.Show();
        }

        private ICommand _deleteCommand;
        private string _AddBttnTitle;
        private string _TitleWindow;

        public ICommand DeleteCommand => _deleteCommand;

        private void Delete(object parameter)
        {
            if (parameter is ExpenseRevenue item)
            {
                Items.Remove(item);
                _expenseRevenue.DeleteRevenue(item.Id);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
