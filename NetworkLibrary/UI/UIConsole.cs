using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLibrary.UI
{
    public class UIConsole
    {
        public static void Print(string message)
        {
            if (OperatingSystem.IsWindows())
            {
                var position = Console.GetCursorPosition();
                int left = position.Left;
                int top = position.Top;
                Console.MoveBufferArea(0, top, left, 1, 0, top + 1);

                Console.SetCursorPosition(0, top);

                Console.WriteLine(message);
                Console.SetCursorPosition(left, top + 1);
            }
            else Console.WriteLine(message);
        }
    }
}
