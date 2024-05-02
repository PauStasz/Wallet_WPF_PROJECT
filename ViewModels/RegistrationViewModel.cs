using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMEssentials.ViewModels;

/*using MVVMEssentials.Commands;
using MVVMEssentials.Services;*/
using System.Windows.Input;

namespace Wallet.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }


        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get
            {
                return _surname;
            }

            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }


        private string _nick;
        public string Nick
        {
            get
            {
                return _nick;
            }

            set
            {
                _nick = value;
                OnPropertyChanged(nameof(Nick));
            }
        }


        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        private string _passwordCheck;
        public string PasswordCheck
        {
            get
            {
                return _passwordCheck;
            }

            set
            {
                _passwordCheck = value;
                OnPropertyChanged(nameof(PasswordCheck));
            }
        }

        public ICommand SubmitCommand { get;  }

        public RegistrationViewModel()
        {
            // to do         
        }





    }
}
