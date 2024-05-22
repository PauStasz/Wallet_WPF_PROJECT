using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Models.users
{
    internal class IdHolder
    {
        private static IdHolder _instance;

        public int Id {  get; set; }
        private IdHolder()
        {
                
        }

        public static IdHolder Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IdHolder();
                }
                return _instance;
            }
        }
    }
}
