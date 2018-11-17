using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Pixo
{
    public static class Internet
    {
        public static bool Check()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}
