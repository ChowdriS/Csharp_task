using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Utils
{
    internal class AuthHandler
    {
        public static bool AdminAuth(string username, string password)
        {
            return username == "admin" && password == "1234";
        }

        public static bool GuestAuth(string username, string password)
        {
            return username == "guest" && password == "guest";
        }
    }
}
