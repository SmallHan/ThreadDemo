using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolDemo1
{
    class Program
    {
        private delegate string RunOnThreadPool(out int threadId);

        private static void Callback(IAsyncResult ar)
        {
            Console.WriteLine("Starting a callback");
            Console.WriteLine($"State passed to c callback:{ar.AsyncState}");
            Console.WriteLine($"Is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Console.WriteLine($"Thread pool worker thread id:{Thread.CurrentThread.ManagedThreadId}");
        }

        private static string Test(out int threadId)
        {
            Console.WriteLine("Starting...");
            Console.WriteLine($"Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            threadId = Thread.CurrentThread.ManagedThreadId;
            return $"Thread pool worker thread is was:{threadId}";
        }

        static void Main(string[] args)
        {
            int threadId = 0;
            RunOnThreadPool poolDelegate = Test;
            var t = new Thread(() => Test(out threadId));
            t.Start();
            t.Join();

            Console.WriteLine($"Thread id:{threadId}");
            IAsyncResult r = poolDelegate.BeginInvoke(out threadId, Callback, "a delegate asynchronous call");
            //r.AsyncWaitHandle.WaitOne();
            Console.WriteLine("666");
            string result = poolDelegate.EndInvoke(out threadId, r);
            Console.WriteLine($"Thread pool worder thread id:{threadId}");
            Console.WriteLine(result);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
