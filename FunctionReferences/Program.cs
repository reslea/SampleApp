using System;
using System.Collections.Generic;
using System.Linq;
using SampleApp;

namespace FunctionReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = new [] { 1, 2, 3, 4, 5 };


            var checkedNumbers = numbers
                .Foreach(Console.WriteLine)
                .OurWhere(number => number > 3);

            foreach (int number in checkedNumbers)
            {
                Console.WriteLine(number);
            }
        }

        public static IEnumerable<int> WhereEven(IEnumerable<int> collection)
        {
            foreach (int item in collection)
            {
                if (item % 2 == 0)
                    yield return item;
            }
        }

        public static IEnumerable<int> WherePositive(IEnumerable<int> collection)
        {
            foreach (int item in collection)
            {
                if (item > 0)
                    yield return item;
            }
        }

        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        public static bool IsPositive(int number)
        {
            return number > 0;
        }

        private static string GetHelloMessage()
        {
            return "Hello World!";
        }

        private static string GetGoodbyeMessage()
        {
            return "Bye-bye world";
        }

        private static void Method()
        {
            throw new NotImplementedException();
        }
    }
}
