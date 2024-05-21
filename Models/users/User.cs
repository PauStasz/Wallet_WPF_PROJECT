using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Wallet.Helpers;
using Wallet.Repositories;
using Wallet.Repositories.IRepositories;

namespace Wallet.Models.Users
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nick {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string HashPassword { get; set; }

        private IUserRepository _userRepository;

        public User()
        {
            _userRepository = new UserRepository();
        }

        public bool Authenticate(string email, string password)
        {
            List<User> users = _userRepository.GetAllData("users");

            var user = users.FirstOrDefault(u => u.Email.Equals(email));
            
            if ((user is not null) && (password is not null))
            {
                var result = Authentication.VerifyPassword(password, user.HashPassword);

                return result;
            }

            return false;
        }

        public bool IsAlreadyCreated(string email)
        {
            List<User> users = _userRepository.GetAllData("users");

            return users.Any(u => u.Email.Equals(email));
        }

        public void Register(string email, string name, string surname, string nick, string password)
        {
            Email = email;
            Name = name;
            Surname = surname;
            Nick = nick;
            Password = password;
            _userRepository.SetData("users", this);
        }
    }
}
