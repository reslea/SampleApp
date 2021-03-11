using System;
using System.Configuration;

namespace Settings
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupConsoleColor();

            Console.WriteLine("Hello World!");
        }

        private static void SetupConsoleColor()
        {
            var configColor = ConfigurationManager.AppSettings["ConsoleColor"];
            
            if (!string.IsNullOrWhiteSpace(configColor) && Enum.TryParse<ConsoleColor>(configColor, out var color))
            {
                Console.ForegroundColor = color;
            }
        }
    }
}
