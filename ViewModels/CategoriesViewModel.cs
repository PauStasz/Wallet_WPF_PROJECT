using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models.Users;
using Wallet.Models;
using System.Windows.Input;
using Wallet.Helpers;

namespace Wallet.ViewModels
{
    internal class CategoriesViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Category _category;
        public event PropertyChangedEventHandler? PropertyChanged;
        public CategoriesViewModel()
        {
            _user = new User();
            _category = new Category();
            _user.GetCurrentUser();
            //SetItems();

        }

        private ICommand _addCategoryCommand;
        public ICommand AddCategoryCommand
        {
            get
            {
                if (_addCategoryCommand == null)
                {
                    _addCategoryCommand = new RelayCommand(execute => AddCategory());
                }

                return _addCategoryCommand;
            }
        }

        private void AddCategory()
        {
            throw new NotImplementedException();
        }
    }
}
