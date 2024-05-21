﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Helpers
{
    public static class Internet
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsConnected()
        {
            int description = 0;
            return InternetGetConnectedState(out description, 0);
        }
    }
}
