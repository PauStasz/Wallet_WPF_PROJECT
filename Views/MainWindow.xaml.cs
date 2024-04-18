using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wallet.Models.Users;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;

namespace Wallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            
            IUserRepository userRepository = new UserRepository();

            User user = new User
            {
                Name = "Anna",
                Surname = "Kwiat",
                Email = "anna12@op.pl",
                Password = "1234",
            };

            userRepository.SetData("users/", user);
        }
    }
}