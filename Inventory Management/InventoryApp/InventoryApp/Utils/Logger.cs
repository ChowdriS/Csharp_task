using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Utils
{
    internal class Logger
    {
        private static readonly string logFile = "log.txt";

        public static void Log(string message)
        {
            File.AppendAllText(logFile, $"{DateTime.Now}: {message}\n");
        }
    }
}
