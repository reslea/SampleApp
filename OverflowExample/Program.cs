using System;

namespace OverflowExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = int.MaxValue;

            checked
            {
                Console.WriteLine(i++);
            }

            Console.WriteLine(i);
        }
    }
}
