using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;

namespace Wallet.ViewModels
{
    class CategoryFormViewModel : INotifyPropertyChanged
    {
        private Category _category;
        private User _user;
        private bool _isEditedOrAddedNew;
        private BitmapImage _icon;


        public CategoryFormViewModel()
        {
            _user = new User();
            _category = new Category();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());
            _addIconCommand = new RelayCommand(execute => AddIcon());

        }


        private void AddIcon()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files (*.png)|*.png";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                _icon = bitmap;
            }     
        }

        private ICommand _addIconCommand;
        public ICommand AddIconCommand
        {
            get
            {
                return _addIconCommand;
            }
        }
        public CategoryFormViewModel(Category category)
        {
            _category = category;
            Name = _category.Name;
            _user = new User();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());
            _addIconCommand = new RelayCommand(execute => AddIcon());
        }

        private string _name;
        private bool _isValidationEnabledForName = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                _isValidationEnabledForName = true;
            }
        }
        public Category GetCategory()
        {
            return _category;
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand;
            }
        }

        private void CreateCategory()
        {
            if (string.IsNullOrEmpty(Name) || Name.Length <= 0)
            {

                _isValidationEnabledForName = true;
                OnPropertyChanged(nameof(Name));
                

            }
            else if (Internet.IsConnected())
            {


                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;

                if (!_isEditedOrAddedNew)
                {
                    _category.Create(Name, _user.Id);
                    msg.Text = "Kategoria została dodana.";

                    var category = _category.GetCategoriesByIdUserName(_user.Id, Name);
                    SaveImage(category);
                }
                else
                {
                    _category.UpdateOne(Name, _user.Id);
                    msg.Text = "Kategoria została edytowana.";

                    SaveImage(_category);
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow is not null)
                {
                    currentWindow.Close();
                }
            }
            else
            {
                Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Brak dostępu do internetu";

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow is not null)
                {

                    currentWindow.Close();

                }
            }
        }

        private void SaveImage(Category category)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string nameFile = _user.Id.ToString() + "_" + category.Id.ToString() + ".png";
            _category.Icon = nameFile;
            string outputPath = Path.Combine(path, "Assets", "UserIcons", nameFile);

            if (!Directory.Exists(Path.Combine(path, "Assets", "UserIcons")))
            {
                Directory.CreateDirectory(Path.Combine(path, "Assets", "UserIcons"));
            }

            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_icon));

            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        private ICommand _cancelCommand;

        public ICommand CancelCommand
        {
            get
            {

                return _cancelCommand;
            }
        }

        private void CancelAction()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
                currentWindow.Close();

            }
        }

        public string Error
        {

            get { throw new NotImplementedException(); }

        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && _isValidationEnabledForName)
                {
                    if (string.IsNullOrEmpty(Name) || Name.Length <= 0)
                    {
                        return "Nie może być puste.";
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Name, @"[A-Z][a-z]+"))
                    {
                        return "Imię powinien składać się z liter, w tym jednej dużej na początku.";
                    }
                    else if (Name.Length > 20)
                    {
                        return "Nie może przekraczać 20 znaków.";
                    }
                }

                return "";
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
