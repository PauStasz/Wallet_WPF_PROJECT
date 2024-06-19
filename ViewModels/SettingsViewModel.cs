using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Models.Users;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;
using Wallet.Views.DialogsWindows;

namespace Wallet.ViewModels
{
    internal class SettingsViewModel : INotifyPropertyChanged
    {
        private User _user;
        private bool _lanEnIsSelected;
        private bool _lanPlIsSelected;
        private ICommand _save;
        private bool _format1IsSelected;
        private bool _format2IsSelected;
        private Settings _settingsManger;
        private string _title1;
        private string _Language;
        private string _timeFormat;
        private string _saver;
        private string _english;
        private string _polish;
        private string _format1;
        private string _format2;

        public SettingsViewModel()
        {
            _user = new User(); 
            _settingsManger = new Settings();
            _user.GetCurrentUser();


            
            if (!(_user.HasCustomSettings))
            {
                _lanPlIsSelected = true;
                _lanEnIsSelected = false;

                _format1IsSelected = true;
                _format2IsSelected = false;

                SetLanguage();
            }
            else
            {
                _settingsManger.GetSettings(_user.Id);

                _lanPlIsSelected = _settingsManger.Language;
                _lanEnIsSelected = !_settingsManger.Language;

                _format1IsSelected = _settingsManger.Format;
                _format2IsSelected = !_settingsManger.Format;


                if(_settingsManger.Language)
                {
                    
                    SetLanguage();

                }
                else
                {
                    
                    _title1 = "LANGUAGE AND LOCATION";
                    _Language = "Language";
                    _timeFormat = "Time";
                    _saver = "SAVE";
                    _english = "ENGLISH";
                    _polish = "POLISH";
                    _format1 = "YEAR-MONTH-DAY-HOUR";
                    _format2 = "DAY-MONTH-YEAR-HOUR";
                }
            }
            _save = new RelayCommand(execute => SaveSettings());
        }

        private void SetLanguage()
        {
            _title1 = "JĘZYK I LOKALIZACJA";
            _Language = "Język";
            _timeFormat = "Format czasu";
            _saver = "ZAPISZ ZMIANY";
            _english = "ANGIELSKI";
            _polish = "POLSKI";
            _format1 = "ROK-MIESIĄC-DZIEŃ-GODZINA";
            _format2 = "DZIEŃ-MIESIĄC-ROK-GODZINA";

        }

        public string Format1
        {
            get { return _format1; }
            set
            {
                if (_format1 != value)
                {
                    _format1 = value;
                    OnPropertyChanged();

                }
            }
        }

        public string Format2
        {
            get { return _format2; }
            set
            {
                if (_format2 != value)
                {
                    _format2 = value;
                    OnPropertyChanged();

                }
            }
        }
        public string English
        {
            get { return _english; }
            set
            {
                if (_english != value)
                {
                    _english = value;
                    OnPropertyChanged();

                }
            }
        }

        public string Polish
        {
            get { return _polish; }
            set
            {
                if (_polish != value)
                {
                    _polish = value;
                    OnPropertyChanged();

                }
            }
        }
        public string Saver
        {
            get { return _saver; }
            set
            {
                if (_saver != value)
                {
                    _saver = value;
                    OnPropertyChanged();

                }
            }
        }

        public string  TimeFormat
        {
            get { return _timeFormat; }
            set
            {
                if (_timeFormat != value)
                {
                    _timeFormat = value;
                    OnPropertyChanged();

                }
            }
        }

        public string Title1
        {
            get { return _title1; }
            set
            {
                if (_title1 != value)
                {
                    _title1 = value;
                    OnPropertyChanged();

                }
            }
        }

        public string Language
        {
            get { return _Language; }
            set
            {
                if (_Language != value)
                {
                    _Language = value;
                    OnPropertyChanged();

                }
            }
        }
        private void SaveSettings()
        {
            IGenericRepository<Settings> repository = new GenericRepository<Settings>();
            IGenericRepository<User> userRepository = new GenericRepository<User>();

            if (Internet.IsConnected())
            {
                if (!_user.HasCustomSettings)
                {
                    Settings newSettings = new Settings();


                    newSettings.IdUser = _user.Id;
                    newSettings.Language = _lanPlIsSelected;
                    newSettings.Format = _format1IsSelected;

                    repository.SetData("settings", newSettings);

                }
                else
                {
                    _user.HasCustomSettings = true;
                    userRepository.UpdateData("users", _user);

                    _settingsManger.Language = _lanPlIsSelected;
                    _settingsManger.Format = _format1IsSelected;

                    repository.UpdateData("settings", _settingsManger);

                }

                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Zapisano ustawienia. Otwórz ponownie okno.";

                InfoWindow window = new InfoWindow();
                window.Show();
            }
            else
            {
               

                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Brak dostępu do internetu";

                InfoWindow window = new InfoWindow();
                window.Show();
            }

        }

        public bool LanEnIsSelected
        {
            get { return _lanEnIsSelected; }
            set
            {
                if (_lanEnIsSelected != value)
                {
                    _lanEnIsSelected = value;
                    OnPropertyChanged();
                    _lanPlIsSelected = !_lanPlIsSelected;
                    
                }
            }
        }

        public bool LanPlnIsSelected
        {
            get { return _lanPlIsSelected; }
            set
            {
                if (_lanPlIsSelected != value)
                {
                    _lanPlIsSelected = value;
                    OnPropertyChanged();
                    _lanEnIsSelected = !_lanEnIsSelected;

                }
            }
        }

        public bool Format1IsSelected
        {
            get { return _format1IsSelected; }
            set
            {
                if (_format1IsSelected != value)
                {
                    _format1IsSelected = value;
                    OnPropertyChanged();
                    _format2IsSelected = !_format2IsSelected;

                }
            }
        }

        public bool Format2IsSelected
        {
            get { return _format2IsSelected; }
            set
            {
                if (_format2IsSelected != value)
                {
                    _format2IsSelected = value;
                    OnPropertyChanged();
                    _format1IsSelected = !_format1IsSelected;

                }
            }
        }

        public ICommand Save
        {
            get
            {
                return _save;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
