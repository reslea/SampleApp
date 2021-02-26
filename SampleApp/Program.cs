using System;

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Collections;
using SampleApp.Logic;

namespace SampleApp
{
    class Program
    {
        private static void Main(string[] args)
        {
            // merge test

            var difference = DateTime.Now - DateTime.Today;
            Console.WriteLine(difference);
            
            Console.WriteLine(DateTime.Now);
            Thread.Sleep(TimeSpan.FromMinutes(5));
            Console.WriteLine(DateTime.Now);

            //Console.WriteLine("abc".Equals(string.Empty, StringComparison.InvariantCultureIgnoreCase));
        }

        private static void ValueReferenceTypeExample()
        {
            var x = 1;
            var y = 1;

            var p1 = new Person();
            var p2 = new Person();

            Console.WriteLine(x == y);
            Console.WriteLine(p1.Equals(p2));
        }

        private static void StringExample()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine($"{string.Empty}");
            Console.WriteLine(new string('*', 10));

            var numbers = new List<int> { 2, 3, 5, 8 };

            Console.WriteLine(string.Join(", ", numbers));

            var sb = new StringBuilder();

            foreach (var number in numbers)
            {
                sb.Append($"{number}, ");
            }

            var result = sb.ToString();
        }

        private int GetFromConsole()
        {
            // TODO: make utility code from this
            //Console.WriteLine("please enter a number: ");
            //var numberStr = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(numberStr) 
            //    || !int.TryParse(numberStr, out var number))
            //{
            //    return;
            //}
            throw new NotImplementedException();
        }
    }
}
