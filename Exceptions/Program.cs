using System;
using System.Dynamic;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a number");
            var maybeNumber = Console.ReadLine();
            if (double.TryParse(maybeNumber, out double number))
            {
                Console.WriteLine($"returned number is {number}");
            }
            else
            {
                Console.WriteLine("failed to get a number");
            }
        }

        private static bool TryGetNumber(out int number)
        {
            try
            {
                Console.WriteLine("please enter a number:");
                number = GetNumber();
                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("oops, not a number");

                number = 0;
                return false;
            }
        }

        private static int GetNumber()
        {
            var fromConsole = Console.ReadLine();

            return int.Parse(fromConsole);
        }
    }

    class NotNumberException : Exception
    {
        public NotNumberException() : base() { }

        public NotNumberException(string message) : base(message) { }
    }
}
