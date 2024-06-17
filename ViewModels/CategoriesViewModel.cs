﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wallet.Models.Users;
using Wallet.Models;
using System.Windows.Input;
using Wallet.Helpers;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Runtime.CompilerServices;
using Wallet.Views.DialogsWindows;
using System.Diagnostics;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows;

namespace Wallet.ViewModels
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        private User _user;
        private Category _category;
        public event PropertyChangedEventHandler? PropertyChanged;
        public CategoriesViewModel()
        {
            _user = new User();
            _category = new Category();
            _user.GetCurrentUser();

            _addCategoryCommand = new RelayCommand(execute => AddCategory());
            _deleteCommand = new RelayCommand(DeleteAccount);
            _editCommand = new RelayCommand(EditCategory);
            

            SetItems();

        }

     

        private ICommand _editCommand;
        public ICommand EditCommand
        {
            get
            {
                return _editCommand;
            }
        }

        private void EditCategory(object parameter)
        {
            if (parameter is Category item)
            {
                OpenEditAccountDialog(UpdateEditedItems, item);
            }
        }

        private void UpdateEditedItems(Category item)
        {
            int index = Items.IndexOf(item);
            if (index > -1)
            {
                string temp = Items[index].Image;
                Items[index].Image = "null";
                Thread.Sleep(1000);
                Items[index].Image = temp;
            }
            
        }
        private void OpenEditAccountDialog(Action<Category> callback, Category item)
        {
            CategoryFormWindow window = new CategoryFormWindow(item);
            bool done = true;
            window.Closed += (sender, args) => callback(window.GetCategory());
            window.Show();
        }

        private void SetItems()
        {
            List<Category> data = _category.GetCategoriesById(_user.Id);

            if (data != null && data.Count() > 0)
            {
                ObservableCollection<Category> temp = [.. data];
                _items = temp;


            }
            else
            {
                Items = new ObservableCollection<Category>();
            }
        }

        private Category selectedItem;
        public Category SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Category> _items;
        public ObservableCollection<Category> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addCategoryCommand;
        public ICommand AddCategoryCommand
        {
            get
            {
                return _addCategoryCommand;
            }
        }

        private void AddCategory()
        {
            OpenAddAccountDialog(UpdateItems);
        }

        private void UpdateItems(Category result)
        {
            if (!string.IsNullOrEmpty(result.Name) && result.Name.Length > 0)
            {
                Items.Add(result);
            }
        }
        private void OpenAddAccountDialog(Action<Category> callback)
        {
            CategoryFormWindow window = new CategoryFormWindow();

            window.Closed += (sender, args) => callback(window.GetCategory());
            window.Show();
        }


        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand;
            }
        }

        private void DeleteAccount(object parameter)
        {
            if (parameter is Category item)
            {
                Items.Remove(item);
                _category.Delete(item.Id);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
