using LiveCharts;
using System.ComponentModel;
using Wallet.Models;

namespace Wallet.ViewModels
{
    public class ForecastViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ChartValues<double> _expensesValues;
        private ChartValues<double> _movingAverageValues;

        public ForecastViewModel()
        {
            ExpensesValues = new ChartValues<double> { 0 };
            MovingAverageValues = new ChartValues<double> { 0 };
        }

        public ForecastViewModel(int accountId)
        {
            LoadData(accountId);
        }

        public ChartValues<double> ExpensesValues
        {
            get { return _expensesValues; }
            set
            {
                _expensesValues = value;
                OnPropertyChanged(nameof(ExpensesValues));
            }
        }

        public ChartValues<double> MovingAverageValues
        {
            get { return _movingAverageValues; }
            set
            {
                _movingAverageValues = value;
                OnPropertyChanged(nameof(MovingAverageValues));
            }
        }

        private void LoadData(int accountId)
        {
            ExpenseRevenue expenseRevenue = new ExpenseRevenue();
            ExpensesValues = new ChartValues<double>(expenseRevenue.GetMonthlyExpenses(accountId));
            MovingAverageValues = new ChartValues<double>(expenseRevenue.GetExpensesMovingAverage(accountId, 3));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

