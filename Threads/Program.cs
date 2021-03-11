using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        private static int maxThreadId = 1;

        static async Task Main(string[] args)
        {
            // before await (syncronous)
            await WriteNumbers(); // maybe on threadPool
            // after await (as continuation)

            WriteDots();
        }

        static async Task WriteNumbers()
        {
            WriteNumbers(null);

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        static void WriteDots()
        {
            for (int i = 0; i < 10; i++)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine($"{threadId}: .");
            }
        }

        static void WriteNumbers(object obj)
        {
            int i = 0;
            while (i < 10)
            {
                var threadId = Thread.CurrentThread.ManagedThreadId;

                if (maxThreadId < threadId)
                {
                    maxThreadId = threadId;
                }

                Console.WriteLine(threadId != 1
                    ? $"\tThread: {threadId} {i++}" 
                    : $"threadId: {threadId} {i++}");
            }
        }
    }
}
