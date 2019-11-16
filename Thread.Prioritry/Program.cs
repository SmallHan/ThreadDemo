using System;
using System.Threading;

namespace ThreadPrioritry
{
    /// <summary>
    /// 线程优先级
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Current thread  priority:{Thread.CurrentThread.Priority}");
            Console.WriteLine($"Running on all cores available");
            RunThreads();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine($"Running on a single core");
            Console.ReadKey();
        }

        static void RunThreads()
        {
            var sample = new ThreadSample();

            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";

            //设置线程优先级
            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;

            threadOne.Start();
            threadTwo.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }
    }
    class ThreadSample
    {
        private bool _isStopped = false;

        public void Stop()
        {
            _isStopped = true;
        }
        public void CountNumbers()
        {
            
            long counter = 0;
            while (!_isStopped)
            {
                counter++;
            }
            Console.WriteLine($"{Thread.CurrentThread.Name}" +
                $"{Thread.CurrentThread.Priority,11} priority" +
                $"has a count={counter}");
        }
    }
}
