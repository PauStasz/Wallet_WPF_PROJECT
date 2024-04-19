using FireSharp.Config;
using FireSharp;

namespace Wallet.Services.FirebaseRealTimeDatabase
{
    internal class FirebaseSetup
    {
        //public
        public FirebaseClient client;

        //private
        private IFirebaseConfig _config;

        public FirebaseSetup()
        {
            _config = new FirebaseConfig
            {
                AuthSecret = "S8FGNt0XY2j26r2vC0yA2PAxCRUSTKUTOScxyszs",
                BasePath = "https://portfel-a3197-default-rtdb.firebaseio.com/",
            };

            try
            {
                client = new FirebaseClient(_config);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wyjątek do stworzenia instacji _client Firebase");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
