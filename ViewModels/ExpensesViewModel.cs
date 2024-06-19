using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;

namespace Wallet.ViewModels
{
    public class ExpensesViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Expense _expense;
        private Settings _settingsManager;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ExpensesViewModel()
        {
            _user = new User();
            _expense = new Expense();
            _settingsManager = new Settings();
            _user.GetCurrentUser();

            _addExpenseCommand = new RelayCommand(execute => AddExpense());
            _deleteCommand = new RelayCommand(DeleteExpense);
            _editCommand = new RelayCommand(EditExpense);

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
                    TitleWindow = "EXPENSES";
                    AddBttnTitle = "ADD NEW EXPENSE";
                }
            }
        }

        private void SetLanguage()
        {
            TitleWindow = "WYDATKI";
            AddBttnTitle = "DODAJ NOWY WYDATEK";
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

        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand;

        private void EditExpense(object parameter)
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
            List<Expense> data = _expense.GetExpensesById(_user.Id);
            Items = new ObservableCollection<Expense>(data);
        }

        private Expense _selectedItem;
        public Expense SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Expense> _items;
        public ObservableCollection<Expense> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addExpenseCommand;
        public ICommand AddExpenseCommand => _addExpenseCommand;

        private void AddExpense()
        {
            var window = new AddExpenseWindow();
            window.Closed += (sender, args) => SetItems();
            window.Show();
        }

        private ICommand _deleteCommand;
        private string _TitleWindow;
        private string _AddBttnTitle;

        public ICommand DeleteCommand => _deleteCommand;

        private void DeleteExpense(object parameter)
        {
            if (parameter is Expense item)
            {
                Items.Remove(item);
                _expense.Delete(item.Id);
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
