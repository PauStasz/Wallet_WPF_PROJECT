using Wallet.Repositories.IRepositories;
using Wallet.Repositories;
using System.ComponentModel;
using Wallet.Models.Users;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

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

        private string _icon;
        public string Icon
        {
            get => _icon;
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    OnPropertyChanged(nameof(Icon));
                }
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public string DeleteTitle
        {
            get { return _deleteTitle; }
            set
            {
                if (_deleteTitle != value)
                {
                    _deleteTitle = value;
                    OnPropertyChanged(nameof(DeleteTitle));

                }
            }
        }

        public string EditTitle
        {
            get { return _editTitle; }
            set
            {
                if (_editTitle != value)
                {
                    _editTitle = value;
                    OnPropertyChanged(nameof(EditTitle));

                }
            }
        }

        private IGenericRepository<Category> _repository = new GenericRepository<Category>();
        private string _deleteTitle;
        private string _editTitle;

        private User _user;
        private Settings _settingsManger;
        public Category()
        {
            _user = new User();
            _settingsManger = new Settings();
            _user.GetCurrentUser();

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }
        }

        public void Create(string name, int idUser)
        {
            Name = name;
            IdUser = idUser;

            _editTitle = null;
            _deleteTitle = null;

            _repository.SetData("categories", this);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }


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

            _editTitle = null;
            _deleteTitle = null;

            _repository.UpdateData("categories", this);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                if (_settingsManger.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";
                }
            }
        }

        internal void Delete(int id)
        {
            _repository.DeleteData("categories", id);
        }
    }
}
