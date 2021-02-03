using System;
using SampleApp.Logic;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICounter counter = new Counter();

            int[] numbers = counter.GetNumbers();

            for(int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
        }
    }
}
