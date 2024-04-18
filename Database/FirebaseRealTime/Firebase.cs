using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace Wallet.Database.FirebaseRealTime
{
    internal class Firebase
    {
        private static Firebase _instance;
        private IFirebaseConfig _config;

        private Firebase()
        {
            _config = new FirebaseConfig
            {
                AuthSecret = "S8FGNt0XY2j26r2vC0yA2PAxCRUSTKUTOScxyszs",
                BasePath = "https://portfel-a3197-default-rtdb.firebaseio.com/",
            };
        }

        public static Firebase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Firebase();
                }

                return _instance;
            }
        }
    }
}
