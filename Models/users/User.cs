namespace Wallet.Models.Users
{
    internal class User : BaseObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nick {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string HashPassword { get; set; }

    }
}
