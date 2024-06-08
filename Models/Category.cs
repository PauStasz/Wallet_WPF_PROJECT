using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.ComponentModel;
using Wallet.Models.Users;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;

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
        public string Icon { get; set; }

        public string Image { get; set; }

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
                var list = category.Where(a => a.IdUser == idUser).ToList();

                string path = AppDomain.CurrentDomain.BaseDirectory;
                string inputPath = Path.Combine(path, "Assets", "UserIcons");

                if (Directory.Exists(inputPath))
                {
                    foreach (Category item in list)
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

            return new List<Category>();
        }

        public Category GetCategoriesByIdUserName(int idUser, string name)
        {

            List<Category> category = _repository.GetAllData("categories");

            if (category is not null)
            {
                return category.FirstOrDefault(a => a.IdUser == idUser && a.Name == name);
            }

            return new Category();
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
