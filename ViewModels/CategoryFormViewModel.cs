using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Xml;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Views.DialogsWindows;

namespace Wallet.ViewModels
{
    class CategoryFormViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private Category _category;
        private User _user;
        private bool _isEditedOrAddedNew;
        private BitmapImage _icon;
        private bool _isUploaded;


        public CategoryFormViewModel()
        {
            _user = new User();
            _category = new Category();
            _user.GetCurrentUser();
            _isEditedOrAddedNew = false;
            _isUploaded = false;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());
            _addIconCommand = new RelayCommand(execute => AddIcon());

            _settingsManager = new Settings();
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
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";
                    IconLabel = "Add icon";

                }
            }

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
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                

                if (bitmap.PixelWidth > 0 && bitmap.PixelHeight > 0)
                {
                    _icon = bitmap;
                    _isUploaded = true;
                    
                }
                else
                { 
                    _isUploaded = false;
                }
                
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

        private Settings _settingsManager;
        public CategoryFormViewModel(Category category)
        {
            _category = category;
            Name = _category.Name;
            _user = new User();
            
            _user.GetCurrentUser();
            _isEditedOrAddedNew = true;
            _isUploaded = false;

            _saveCommand = new RelayCommand(execute => CreateCategory());
            _cancelCommand = new RelayCommand(execute => CancelAction());
            _addIconCommand = new RelayCommand(execute => AddIcon());
            
            _settingsManager = new Settings();
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
                    NameLabel = "Name";
                    SaveLabel = "Save";
                    CancelLabel = "Cancel";
                    IconLabel = "Add icon";

                }
            }
        }

        private void SetLanguage()
        {
            NameLabel = "Nazwa";
            SaveLabel = "Zapisz";
            CancelLabel = "Anuluj";
            IconLabel = "Dodaj ikonę";
        }

        public string NameLabel
        {
            get => _NameLabel;
            set
            {
                _NameLabel = value;
                OnPropertyChanged(nameof(NameLabel));
            }
        }

        public string SaveLabel
        {
            get => _SaveLabel;
            set
            {
                _SaveLabel = value;
                OnPropertyChanged(nameof(SaveLabel));
            }
        }

        public string CancelLabel
        {
            get => _CancelLabel;
            set
            {
                _CancelLabel = value;
                OnPropertyChanged(nameof(CancelLabel));
            }
        }

        public string IconLabel
        {
            get => _IconLabel;
            set
            {
                _IconLabel = value;
                OnPropertyChanged(nameof(IconLabel));
            }
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

                if (_isUploaded)
                {
                    if (!_isEditedOrAddedNew)
                    {
                        _category.Create(Name, _user.Id);
                        

                        if (!(_user.HasCustomSettings))
                        {

                            msg.Text = "Kategoria została dodana.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                msg.Text = "Kategoria została dodana.";

                            }
                            else
                            {
                                msg.Text = "The category has been added.";

                            }
                        }

                        var category = _category.GetCategoriesByIdUserName(_user.Id, Name);
                        SaveImage(category);
                    }
                    else
                    {
                        _category.UpdateOne(Name, _user.Id);
                       

                        if (!(_user.HasCustomSettings))
                        {

                            msg.Text = "Kategoria została edytowana.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                msg.Text = "Kategoria została edytowana.";

                            }
                            else
                            {
                                msg.Text = "The category has been edited.";

                            }
                        }

                        SaveImage(_category);
                    }

                    InfoWindow window = new InfoWindow();
                    window.Show();

                    if (currentWindow != null)
                    {
                        currentWindow.Close();
                    }
                }
                else
                {
                    Window currentWindow1 = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);


                    MessageHolder msg1 = MessageHolder.Instance;
                    if (!(_user.HasCustomSettings))
                    {

                        msg1.Text = "Brak zdjęcia";
                    }
                    else
                    {
                        _settingsManager.GetSettings(_user.Id);

                        if (_settingsManager.Language)
                        {

                            msg1.Text = "Brak zdjęcia";

                        }
                        else
                        {
                            msg1.Text = "No photo";

                        }
                    }

                    InfoWindow window1 = new InfoWindow();
                    window1.Show();

                    if (currentWindow1 is not null)
                    {

                        currentWindow1.Close();

                    }
                }

            }
            else
            {
                Window currentWindow2 = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                MessageHolder msg2 = MessageHolder.Instance;
                if (!(_user.HasCustomSettings))
                {

                    msg2.Text = "Brak dostępu do internetu";
                }
                else
                {
                    _settingsManager.GetSettings(_user.Id);

                    if (_settingsManager.Language)
                    {

                        msg2.Text = "Brak dostępu do internetu";

                    }
                    else
                    {
                        msg2.Text = "No internet access.";

                    }
                }

                InfoWindow window = new InfoWindow();
                window.Show();

                if (currentWindow2 != null)
                {

                    currentWindow2.Close();

                }
            }
        }

        private void SaveImage(Category category)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            Random rand = new Random();
            int version = rand.Next(1,10);
            string nameFile = _user.Id.ToString() + "_" + category.Id.ToString() + "_" + version + ".png";
            _category.Icon = nameFile;
            string outputPath = Path.Combine(path, "Assets", "UserIcons", nameFile);


            if (!Directory.Exists(Path.Combine(path, "Assets", "UserIcons")))
            {
                Directory.CreateDirectory(Path.Combine(path, "Assets", "UserIcons"));
            }
             
            try
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(_icon));
                FileStream fileStream = new FileStream(outputPath, FileMode.Create);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch (IOException ex)
            {
                Debug.Write(ex.ToString());
            }

            _category.Image = outputPath;
        }

        private ICommand _cancelCommand;
        private string _NameLabel;
        private string _SaveLabel;
        private string _CancelLabel;
        private string _IconLabel;

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
            get
            {
                return string.Empty;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && _isValidationEnabledForName)
                {
                    if (string.IsNullOrEmpty(Name) || Name.Length <= 0)
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Nie może być puste.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Nie może być puste.";

                            }
                            else
                            {
                                temp = "It can't be empty.";

                            }
                        }

                        return temp;
                    }
                    else if (!System.Text.RegularExpressions.Regex.IsMatch(Name, @"[A-Z][a-z]+"))
                    {

                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Imię powinien składać się z liter, w tym jednej dużej na początku.";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Imię powinien składać się z liter, w tym jednej dużej na początku.";

                            }
                            else
                            {
                                temp = "The name should consist of letters, including one capital letter at the beginning.";

                            }
                        }

                        return temp;
                        
                    }
                    else if (Name.Length > 20)
                    {
                        string temp = string.Empty;
                        if (!(_user.HasCustomSettings))
                        {

                            temp = "Wartość musi być mniejsza od 20";
                        }
                        else
                        {
                            _settingsManager.GetSettings(_user.Id);

                            if (_settingsManager.Language)
                            {

                                temp = "Wartość musi być mniejsza od 20";

                            }
                            else
                            {
                                temp = "The value must be less than 20";

                            }
                        }

                        return temp;

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
