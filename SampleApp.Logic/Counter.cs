using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Logic
{
    public class Counter : ICounter
    {
        public int[] GetNumbers()
        {
            int[] numbers = new int[100];
            for (int i = 0; i < 100; i++)
            {
                AssignNumber(numbers, i);
            }

            return numbers;
        }

        public void AssignNumber(int[] numbers, int index)
        {
            numbers[index] = index + 1;
        }
    }
}
