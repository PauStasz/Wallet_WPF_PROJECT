using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.ComponentModel;
using System.IO;

namespace Wallet.Models
{
    public class Expense : BaseEntity, INotifyPropertyChanged
    {
        private string _name;
        private decimal _amount;
        private DateTime _date;
        private string _category;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged(nameof(Category));
                }
            }
        }

        public int IdUser { get; set; }
        public string Image { get; set; }

        private IGenericRepository<Expense> _repository = new GenericRepository<Expense>();

        public void Create(string name, decimal amount, DateTime date, string category, int idUser)
        {
            Name = name;
            Amount = amount;
            Date = date;
            Category = category;
            IdUser = idUser;

            _repository.SetData("expenses", this);
        }

        public List<Expense> GetExpensesById(int idUser)
        {
            List<Expense> expenses = _repository.GetAllData("expenses");

            if (expenses is not null)
            {
                var list =  expenses.Where(e => e.IdUser == idUser).ToList();

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string inputPath = Path.Combine(path, "Assets", "UserIcons");

                if (!Directory.Exists(inputPath))
                {
                    foreach (Expense item in list)
                    {
                        string nameFile = idUser.ToString() + "_" + item.Id.ToString() + ".png";
                        string fullPath = System.IO.Path.Combine(inputPath, nameFile);
                        
                        if (File.Exists(fullPath))
                        {
                            item.Image = fullPath;
                        }
                    }

                }

                return list;

            }

            return new List<Expense>();
        }


        public Expense GetExpensesByIdUserName(int idUser, string name)
        {

            List<Expense> expense = _repository.GetAllData("expenses");

            if (expense is not null)
            {
                return expense.FirstOrDefault(a => a.IdUser == idUser && a.Name == name);
            }

            return new Expense();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void UpdateOne(string name, decimal amount, DateTime date, string category, int id)
        {
            Name = name;
            Amount = amount;
            Date = date;
            Category = category;
            IdUser = id;

            _repository.UpdateData("expenses", this);
        }

        internal void Delete(int id)
        {
            _repository.DeleteData("expenses", id);
        }


    }
}

