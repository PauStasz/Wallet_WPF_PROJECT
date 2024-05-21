using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wallet.Helpers;
using Wallet.Models;
using Wallet.Views.LoginRegistrationSystemViews;

namespace Wallet.ViewModels
{
    internal class InfoWindowViewModel : INotifyPropertyChanged
    {
        private MessageHolder _message2;
        private string _message = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public InfoWindowViewModel()
        {
            _message2 = MessageHolder.Instance;
            _message = _message2.Text;

        }
        internal void SetMessage(string message)
        {
            _message = message;
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICommand _okCommand;

        public ICommand OkCommand
        {
            get
            {
                if (_okCommand == null)
                {
                    _okCommand = new RelayCommand(execute => Agreement());
                }

                return _okCommand;
            }
        }

        private void Agreement()
        {
            Window currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (currentWindow is not null)
            {
               currentWindow.Close();
                
            }
        }
    }
}
