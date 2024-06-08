using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wallet.Models;
using Wallet.ViewModels;

namespace Wallet.Views.DialogsWindows
{
    /// <summary>
    /// Logika interakcji dla klasy CategoryFormWindow.xaml
    /// </summary>
    public partial class CategoryFormWindow : Window
    {
        CategoryFormViewModel _viewModel;
        public CategoryFormWindow()
        {
            InitializeComponent();
            _viewModel = new CategoryFormViewModel();
            DataContext = _viewModel;
        }

        public CategoryFormWindow(Category category)
        {
            InitializeComponent();
            _viewModel = new CategoryFormViewModel(category);
            DataContext = _viewModel;
        }
        public Category GetCategory()
        {
            return _viewModel.GetCategory();
        }

    }
}
