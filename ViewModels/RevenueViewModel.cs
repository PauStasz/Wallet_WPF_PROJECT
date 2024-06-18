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

        public event PropertyChangedEventHandler? PropertyChanged;

        public RevenueViewModel()
        {
            _user = new User();
            _expenseRevenue = new ExpenseRevenue();
            _account = new Account();
            _user.GetCurrentUser();
            _account.GetMainActive(_user.Id);

            _addRevenueCommand = new RelayCommand(execute => Add());
            _deleteCommand = new RelayCommand(Delete);
            _editCommand = new RelayCommand(Edit);

            SetItems();
        }

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand;

        private void Edit(object parameter)
        {
            if (parameter is Expense item)
            {
                AddExpenseWindow window = new AddExpenseWindow
                {
                    DataContext = new AddExpenseViewModel(item)
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
            var window = new AddExpenseWindow();
            window.Closed += (sender, args) => SetItems();
            window.Show();
        }

        private ICommand _deleteCommand;
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
