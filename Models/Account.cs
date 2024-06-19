using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Wallet.Models.Users;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;

namespace Wallet.Models
{
    public class Account : BaseEntity, INotifyPropertyChanged
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

        private double _salary;
        public double Salary
        {
            get => _salary;
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    OnPropertyChanged(nameof(Salary));
                }
            }
        }

        public int IdUser { get; set; }

        public bool IsMain { get; set; }

        private string _mainAccount;
        public string MainAccount
        {
            get => _mainAccount;
            set
            {
                if (_mainAccount != value)
                {
                    _mainAccount = value;
                    OnPropertyChanged(nameof(MainAccount));
                }
            }
        }

        private User _user;
        private Settings _settingsManager;

        public Account()
        {
            _user = new User();
            _settingsManager = new Settings();

            

            _user = new User();
            _settingsManager = new Settings();
            _user.GetCurrentUser();

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";

                if (!IsMain)
                    _mainAccount = "USTAW JAKO GŁÓWNE";
                else
                    _mainAccount = "WYBRANO NA GŁÓWNE";
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                    if (!IsMain)
                        _mainAccount = "USTAW JAKO GŁÓWNE";
                    else
                        _mainAccount = "WYBRANO NA GŁÓWNE";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";

                    if (!IsMain)
                        _mainAccount = "SET AS MAIN";
                    else
                        _mainAccount = "SELECTED FOR MAIN";
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



        private IGenericRepository<Account> _repository = new GenericRepository<Account>();
        private string _editTitle;
        private string _deleteTitle;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Create(string name, double salary, int idUser)
        {
            Name = name;
            Salary = salary;
            IdUser = idUser;
            IsMain = false;

            _deleteTitle = null;
            _editTitle = null;

            _repository.SetData("accounts", this);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";

                if (!IsMain)
                    _mainAccount = "USTAW JAKO GŁÓWNE";
                else
                    _mainAccount = "WYBRANO NA GŁÓWNE";
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                    if (!IsMain)
                        _mainAccount = "USTAW JAKO GŁÓWNE";
                    else
                        _mainAccount = "WYBRANO NA GŁÓWNE";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";

                    if (!IsMain)
                        _mainAccount = "SET AS MAIN";
                    else
                        _mainAccount = "SELECTED FOR MAIN";
                }
            }
        }

        public List<Account> GetAccountsById(int idUser)
        {

            List<Account> accounts = _repository.GetAllData("accounts");

            if (accounts != null) 
            {
                return accounts.Where(a => a.IdUser == idUser).ToList();
            }

            return new List<Account>();
        }

        public void Delete(int id)
        {
            _repository.DeleteData("accounts", id);
        }

        public void UpdateAll(ObservableCollection<Account> items)
        {

            foreach (Account account in items)
            {
                _repository.UpdateData("accounts", account);
            }
        }

        public void UpdateOne(string name, double salary, int id)
        {

            Name = name;
            Salary = salary;
            IdUser = id;

            _editTitle = null;
            _deleteTitle = null;

            _repository.UpdateData("accounts", this);

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";

                if (!IsMain)
                    _mainAccount = "USTAW JAKO GŁÓWNE";
                else
                    _mainAccount = "WYBRANO NA GŁÓWNE";
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                    if (!IsMain)
                        _mainAccount = "USTAW JAKO GŁÓWNE";
                    else
                        _mainAccount = "WYBRANO NA GŁÓWNE";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";

                    if (!IsMain)
                        _mainAccount = "SET AS MAIN";
                    else
                        _mainAccount = "SELECTED FOR MAIN";
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void GetMainActive(int id)
        {
            List<Account> accounts = _repository.GetAllData("accounts");

            Account temp = accounts.FirstOrDefault(a => a.IsMain == true && a.IdUser == id);

            if (temp != null)
            {
                Name = temp.Name;
                Salary = temp.Salary;
                IdUser = temp.IdUser;
                IsMain = temp.IsMain;
                MainAccount = temp.MainAccount;
                Id = temp.Id;
            }

            if (!(_user.HasCustomSettings))
            {
                _editTitle = "EDYTUJ";
                _deleteTitle = "USUŃ";

                if (!IsMain)
                    _mainAccount = "USTAW JAKO GŁÓWNE";
                else
                    _mainAccount = "WYBRANO NA GŁÓWNE";
            }
            else
            {
                _settingsManager.GetSettings(_user.Id);

                if (_settingsManager.Language) //polish
                {

                    _editTitle = "EDYTUJ";
                    _deleteTitle = "USUŃ";

                    if (!IsMain)
                        _mainAccount = "USTAW JAKO GŁÓWNE";
                    else
                        _mainAccount = "WYBRANO NA GŁÓWNE";

                }
                else//engslish
                {
                    _editTitle = "EDIT";
                    _deleteTitle = "DELETE";

                    if (!IsMain)
                        _mainAccount = "SET AS MAIN";
                    else
                        _mainAccount = "SELECTED FOR MAIN";
                }
            }


        }
    }
}
