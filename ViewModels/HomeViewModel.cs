using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.Diagnostics;
using System.Windows.Input;
using Wallet.Helpers;

namespace Wallet.ViewModels
{
    internal class HomeViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Account _account;
        private ExpenseRevenue _expense;

        private ICommand _expanses;
        private ICommand _futureExpanses;
        private ICommand _revenues;
        public HomeViewModel()
        {
            _user = new User();
            _account = new Account();
            _expense = new ExpenseRevenue();

            _user.GetCurrentUser();
            _account.GetMainActive(_user.Id);

            _expanses = new RelayCommand(execute => SetListToExpanses());
            _futureExpanses = new RelayCommand(execute => SetListToFutureExpanses());
            _revenues = new RelayCommand(execute => SetListToRevenues());

            SetListToExpanses();
        }

        private void SetListToRevenues()
        {
            List<ExpenseRevenue> data = _expense.GetAllRevenuesByAccountId(_account.Id);


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

        private void SetListToFutureExpanses()
        {
            List<ExpenseRevenue> data = _expense.GetAllExpensesByAccountId(_account.Id).Where(d => d.Date > DateTime.Now).ToList();


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


        private void SetListToExpanses()
        {

            List<ExpenseRevenue> data = _expense.GetAllExpensesByAccountId(_account.Id).Where(d => d.Date <= DateTime.Now).ToList();


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

        public ICommand ExpansesCommand
        {
            get
            { 
                return _expanses;
            }
        }

        public ICommand FutureExpansesCommand
        {
            get
            {
                return _futureExpanses;
            }
        }

        public ICommand RevenuesCommand
        {
            get
            {
                return _revenues;
            }
        }
        public Account Account { get { return _account; } }

        private ObservableCollection<ExpenseRevenue> _items;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ExpenseRevenue> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private ExpenseRevenue selectedItem;

        public ExpenseRevenue SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

       
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
