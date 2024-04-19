using System.Diagnostics;
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
            
            IGenericRepository<User> userRepository = new GenericRepository<User>();

            User user = new User
            {
                Id = 0,
                Name = "Kasia",
                Surname = "Kwiat",
                Email = "anna12@op.pl",
                Password = "1234",
            };

            User user2 = new User
            {
                Id = 1,
                Name = "Ola",
                Surname = "Kdt",
                Email = "a156789@op.pl",
                Password = "123fgh4",
            };


            // userRepository.SetData("users", user);
            //userRepository.SetData("users", user2);

            userRepository.UpdateData("users", user2);
            // userRepository.DeleteData("users", 0);

        }
    }
}