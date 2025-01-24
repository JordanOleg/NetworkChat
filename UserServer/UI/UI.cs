using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.UI
{
    internal class UI
    {
        public static void StartServer()
        {
            Console.WriteLine("Statring...");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Server ready");
            Console.WriteLine("Listening to connections");
            Console.ResetColor();
        }

    }
}
