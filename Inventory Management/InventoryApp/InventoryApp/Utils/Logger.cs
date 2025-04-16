using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Utils
{
    internal class Logger
    {
        private static readonly string logFile = "D:\\Presidio\\Csharp_task\\Inventory Management\\InventoryApp\\InventoryApp\\log.txt";

        public void Log(string message)
        {
            try
            {
                File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");

            }
            catch (Exception e)
            {
                Console.WriteLine( e.Message );
            }
        }
    }
}
