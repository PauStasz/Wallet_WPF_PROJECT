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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wallet.Themes.UserControls
{
    /// <summary>
    /// Logika interakcji dla klasy Searchbar.xaml
    /// </summary>
    public partial class Searchbar : UserControl
    {
        public Searchbar()
        {
            InitializeComponent();
        }

        private void SeachText(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxSearch.Text))
            {
                TextBlockSearch.Visibility = Visibility.Visible;
                
            }
            else
            {
                TextBlockSearch.Visibility = Visibility.Collapsed;
            }
        }
    }
}
