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
            var ages = new [] { 20, 18, 15, 13, 25, 21, 54 };
            
            var adults = ages
                .Where(a => a > 18)
                .OrderBy(a => a)
                .Foreach(Console.WriteLine);

            foreach (int age in adults)
            {
                Console.WriteLine(age);
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
