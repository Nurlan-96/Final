using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApplication.Utilities
{
    public class Helper
    {
        public static void ChangeTextColor(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    public enum Menus
        {
            Create=1,
            View,
            Update,
            Delete
        }
    }
}
