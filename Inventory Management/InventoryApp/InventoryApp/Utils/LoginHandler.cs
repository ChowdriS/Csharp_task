using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Utils
{
    internal class LoginHandler
    {
        public delegate bool AuthDelegate(string username, string password);
        public static bool Login(AuthDelegate authMethod)
        {
            Console.WriteLine(" Login Required");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            return authMethod(username, password);
        }
    }
}
