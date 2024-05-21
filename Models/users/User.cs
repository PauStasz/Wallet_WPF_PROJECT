using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
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

            if ((user is not null) && (Password is not null))
            {
                var result = Authentication.VerifyPassword(password, user.HashPassword);

                return result;
            }

            return false;
        }

        public bool IsAlreadyCreated()
        {
            List<User> users = _userRepository.GetAllData("users");

            return users.Any(u => u.Email.Equals(Email));
        }

        public void Register()
        {
            _userRepository.SetData("users", this);
        }
    }
}
