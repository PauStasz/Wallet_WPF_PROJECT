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

                _title1 = "JĘZYK I LOKALIZACJA";
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
                    _title1 = "JĘZYK I LOKALIZACJA";

                }
                else
                {
                    _title1 = "LANGUAGE AND LOCATION";
                }
            }
            _save = new RelayCommand(execute => SaveSettings());
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
        private void SaveSettings()
        {
            IGenericRepository<Settings> repository = new GenericRepository<Settings>();
            IGenericRepository<User> userRepository = new GenericRepository<User>();

            if (Internet.IsConnected())
            {
                if (!_user.HasCustomSettings)
                {
                    _user.HasCustomSettings = true;
                    userRepository.UpdateData("users", _user);

                    _settingsManger.Language = _lanPlIsSelected;
                    _settingsManger.Format = _format1IsSelected;

                    repository.UpdateData("settings", _settingsManger);
                }
                else
                {
                    Settings newSettings = new Settings();

                    newSettings.IdUser = _user.Id;
                    newSettings.Language = _lanPlIsSelected;
                    newSettings.Format = _format1IsSelected;

                    repository.SetData("settings", newSettings);

                }

                MessageHolder msg = MessageHolder.Instance;
                msg.Text = "Zapisano ustawienia. Otwórz pownonwie okno.";

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
