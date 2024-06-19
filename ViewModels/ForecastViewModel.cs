using LiveCharts.Wpf;
using LiveCharts;
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

namespace Wallet.ViewModels
{
    internal class ForecastViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Expense _expense;
        private Settings _settingsManager;

        public event PropertyChangedEventHandler? PropertyChanged;

        public SeriesCollection ChartSeries { get; set; }
        public SeriesCollection ChartSeries2 { get; set; }
        public SeriesCollection ChartSeries3 { get; set; }


        public ForecastViewModel()
        {
            _user = new User();
            _expense = new Expense();
            _settingsManager = new Settings();
            _user.GetCurrentUser();

            InitializeChart();

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
                    _MonthLabels = "January, March, May, July, September, November";
                    _WeeksLabels = "week1, week2, week3, week4, week5, week6";
                    _Label = "Statistics";
                    _WeekLabel = "Weekly";
                    _MonthLabel = "Monthly";
                    _ForecastLabel = "Forecast";
                }
            }
        }

        private void SetLanguage()
        {
            _MonthLabels = "Styczeń, Marzec, Maj, Lipiec, Wrzesień, Listopad";
            _WeeksLabels = "tydz1, tydz2, tydz3, tydz4, tydz5, tydz6";
            _Label = "Statystyki";
            _WeekLabel = "Tygodniowo";
            _MonthLabel = "Miesięcznie";
            _ForecastLabel = "Prognoza";
        }

        public string ForecastLabel
        {
            get => _ForecastLabel;
            set
            {
                _ForecastLabel = value;
                OnPropertyChanged();
            }
        }
        public string Label
        {
            get => _Label;
            set
            {
                _Label = value;
                OnPropertyChanged();
            }
        }

        public string MonthLabel
        {
            get => _MonthLabel;
            set
            {
                _MonthLabel = value;
                OnPropertyChanged();
            }
        }
        public string WeekLabel
        {
            get => _WeekLabel;
            set
            {
                _WeekLabel = value;
                OnPropertyChanged();
            }
        }

        public string MonthLabels
        {
            get => _MonthLabels;
            set
            {
                _MonthLabels = value;
                OnPropertyChanged();
            }
        }
        public string WeeksLabels
        {
            get => _WeeksLabels;
            set
            {
                _WeeksLabels = value;
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
        private void InitializeChart()
        {
            ChartSeries = new SeriesCollection();
            LineSeries movingAverageSeries = new LineSeries
            {
                Title = "Moving Average",
                Values = new ChartValues<double>()

            };
            ChartSeries.Add(movingAverageSeries);

            ChartSeries2 = new SeriesCollection();
            LineSeries weeklyExpensesSeries = new LineSeries
            {
                Title = "Weekly expenses",
                Values = new ChartValues<double>()

            };
            ChartSeries2.Add(weeklyExpensesSeries);

            ChartSeries3 = new SeriesCollection();
            LineSeries monthlyExpensesSeries = new LineSeries
            {
                Title = "Monthly expenses",
                Values = new ChartValues<double>()

            };
            ChartSeries3.Add(monthlyExpensesSeries);
        }

        private void SetItems()
        {
            List<Expense> data = _expense.GetExpensesById(_user.Id);
            Items = new ObservableCollection<Expense>(data);
            UpdateChartData(data);
        }

        private void UpdateChartData(List<Expense> expenses)
        {
            ChartSeries[0].Values.Clear();
            foreach (var expense in expenses)
            {
                ChartSeries[0].Values.Add((double)expense.Amount);
            }
            CalculateMovingAverage(expenses);

            ChartSeries2[0].Values.Clear();
            foreach (var expense in expenses)
            {
                ChartSeries2[0].Values.Add((double)expense.Amount);
            }
            WeeklyAverage(expenses);

            ChartSeries3[0].Values.Clear();
            foreach (var expense in expenses)
            {
                ChartSeries3[0].Values.Add((double)expense.Amount);
            }
            MonthlyAverage(expenses);
        }

        private void CalculateMovingAverage(List<Expense> expenses)
        {
            int period = 3;

            var monthlyExpenses = expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new { Date = new DateTime(g.Key.Year, g.Key.Month, 1), Total = g.Sum(e => (double)e.Amount) })
                .OrderBy(m => m.Date)
                .ToList();

            List<double> movingAverages = new List<double>();
            List<string> xLabels = new List<string>();

            for (int i = 0; i < monthlyExpenses.Count; i++)
            {
                if (i < period - 1)
                {
                    movingAverages.Add(0);
                    xLabels.Add(monthlyExpenses[i].Date.ToString("MM"));
                }
                else
                {
                    double sum = 0;
                    for (int j = 0; j < period; j++)
                    {
                        sum += monthlyExpenses[i - j].Total;
                    }
                    movingAverages.Add(sum / period);
                    xLabels.Add(monthlyExpenses[i].Date.ToString("MM"));
                }
            }

            ChartSeries[0].Values.Clear();
            foreach (var average in movingAverages)
            {
                var tmp = Math.Round(average);
                ChartSeries[0].Values.Add(tmp);
            }
        }

        private void MonthlyAverage(List<Expense> expenses)
        {
            var monthlyExpenses = expenses
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new { Date = new DateTime(g.Key.Year, g.Key.Month, 1), Total = g.Sum(e => (double)e.Amount) })
                .OrderBy(m => m.Date)
                .ToList();





            ChartSeries3[0].Values.Clear();
            foreach (var exp in monthlyExpenses)
            {
                var tmp = Math.Round(exp.Total);
                ChartSeries3[0].Values.Add(tmp);
            }
        }

        private void WeeklyAverage(List<Expense> expenses)
        {
            int GetWeekOfYear(DateTime date)
            {
                var cultureInfo = System.Globalization.CultureInfo.CurrentCulture;
                var calendar = cultureInfo.Calendar;
                var calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
                var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
                return calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);
            }

            var weeklyExpenses = expenses
                .GroupBy(e => new { e.Date.Year, Week = GetWeekOfYear(e.Date) })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Week = g.Key.Week,
                    Total = g.Sum(e => (double)e.Amount)
                })
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Week)
                .ToList();

            ChartSeries2[0].Values.Clear();
            foreach (var exp in weeklyExpenses)
            {
                var roundedTotal = Math.Round(exp.Total);
                ChartSeries2[0].Values.Add(roundedTotal);
            }
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
        private string _MonthLabels;
        private string _WeeksLabels;
        private string _Label;
        private string _WeekLabel;
        private string _MonthLabel;
        private string _ForecastLabel;

        public ICommand DeleteCommand => _deleteCommand;

        private void DeleteExpense(object parameter)
        {
            if (parameter is Expense item)
            {
                Items.Remove(item);
                _expense.Delete(item.Id);
                UpdateChartData(Items.ToList());
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}