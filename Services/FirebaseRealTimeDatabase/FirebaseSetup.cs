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
                AuthSecret = "1I9XMHVuVBwOWAYig334luwhj3OFzm4btVRh12Ne",
                BasePath = "https://portfel-a3000-default-rtdb.firebaseio.com/",
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
