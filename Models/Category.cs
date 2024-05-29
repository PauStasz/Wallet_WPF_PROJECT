using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.ComponentModel;
using Wallet.Models.Users;

namespace Wallet.Models
{
    public class Category : BaseEntity, INotifyPropertyChanged
    {
        private string _name;

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

        public int IdUser { get; set; }

        private IGenericRepository<Category> _repository = new GenericRepository<Category>();

        public void Create(string name, int idUser)
        {
            Name = name;
            IdUser = idUser;

            _repository.SetData("categories", this);
        }

        public List<Category> GetCategoriesById(int idUser)
        {

            List<Category> category = _repository.GetAllData("categories");

            if (category is not null)
            {
                return category.Where(a => a.IdUser == idUser).ToList();
            }

            return new List<Category>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void UpdateOne(string name, int id)
        {
            Name = name;
            IdUser = id;

            _repository.UpdateData("categories", this);
        }

        internal void Delete(int id)
        {
            _repository.DeleteData("categories", id);
        }
    }
}
