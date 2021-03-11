using System;

namespace Logs
{
    class Program
    {
        private static readonly Guid UniqueIdentifier = Guid.NewGuid();

        static void Main(string[] args)
        {
            WriteLog("A number was asked to square");
            if (int.TryParse(Console.ReadLine(), out var number))
            {
                WriteLog($"the result is {Math.Pow(int.MaxValue, 2)}");
            }
            else
            {
                WriteLog("its not a number");
            }
        }

        private static void WriteLog(string message)
        {
            Console.WriteLine($"{UniqueIdentifier}: {message}");
        }
    }
}
