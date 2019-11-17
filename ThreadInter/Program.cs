using System;
using System.Threading;

namespace ThreadInter
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Counter();
            var t1 = new Thread(() => TestController(c));
            var t2 = new Thread(() => TestController(c));
            var t3 = new Thread(() => TestController(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"Total count :{c.Count}");

            Console.WriteLine("----------华丽的分割线-------------");

            Console.WriteLine($"Correct counter");

            var c1 = new CounterNoLock();
            t1 = new Thread(() => TestController(c1));
            t2 = new Thread(() => TestController(c1));
            t3 = new Thread(() => TestController(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"Total count :{c1.Count}");
            Console.ReadKey();
        }

        static void TestController(CounterBase c)
        {
            for (var i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }
    }
    abstract class CounterBase
    {
        public abstract void Increment();
        public abstract void Decrement();
    }
    class Counter : CounterBase
    {
        public int Count { get; private set; }

        public override void Increment()
        {
            Count++;
        }
        public override void Decrement()
        {
            Count--;
        }
    }

    class CounterNoLock : CounterBase
    {
        private int _count;
        public int Count => _count;

        //Interlocked  无需锁定任何对象既可获取正确的结果
        public override void Increment()
        {
            Interlocked.Increment(ref _count);
        }
        public override void Decrement()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
