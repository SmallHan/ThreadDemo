using System;
using System.Threading;

namespace ThreadPrams
{
    /// <summary>
    /// 向线程传递参数
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ThreadSample sample = new ThreadSample(10);
            Thread threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            threadOne.Start();
            threadOne.Join();
            Console.WriteLine("-------华丽得分割线-------");

            Thread threadTwo = new Thread(Count);
            threadTwo.Name = "ThreadTwo";
            threadTwo.Start(8);
            threadTwo.Join();
            Console.WriteLine("-------华丽得分割线-------");

            Thread threadThree = new Thread(()=> CountNumbers(12));
            threadThree.Name = "ThreadThree";
            threadThree.Start();
            threadThree.Join();
            Console.WriteLine("-------华丽得分割线-------");

            //打印出20，因为在执行线程前值已经改为20了
            int i = 10;
            var threadFour = new Thread(()=> PrintNumber(i));
            i = 20;
            var threadFive = new Thread(() => PrintNumber(i));
            threadFour.Start();
            threadFive.Start();
            Console.ReadKey();
        }

        static void Count(object iterations)
        {
            CountNumbers((int)iterations);
        }
        static void CountNumbers(int iterations)
        {
            for (var i = 1; i < iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine($"{Thread.CurrentThread.Name} prints {i}");
            }
        }
        static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }
    }
    class ThreadSample
    {
        private readonly int _iterations;
        public ThreadSample(int iterations)
        {
            _iterations = iterations;
        }
        public void CountNumbers()
        {
            for (var i = 1; i < _iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine($"{Thread.CurrentThread.Name} prints {i}");
            }
        }
    }
}
