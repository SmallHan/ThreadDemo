 using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSemap
{
    class Program
    {
        static SemaphoreSlim _semaphore = new SemaphoreSlim(3);

        static void ReadDataBase(string name, int seconds)
        {
            _semaphore.Wait();
            Trace.WriteLine($"{name}进入==>{DateTime.Now}");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Trace.WriteLine($"{name}释放==>{DateTime.Now}");
            _semaphore.Release();
        }
        static void Main(string[] args)
        {
            var task = new Task[6];
            for (var i = 1; i <= 6; i++)
            {
                string threadName = $"Thread:{i}";
                int secondsWait = i * 2;
                task[i - 1] = Task.Run(() => ReadDataBase(threadName, secondsWait));
            }
            Console.ReadKey();
        }

    }


   
}
