using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallel
{
    class Program
    {

        static async Task AsynchronousProcessing()
        {
            Task<string> t1 = GetInfoAsync("Task 1", 3);
            Task<string> t2 = GetInfoAsync("Task 2", 5);
            string[] results = await Task.WhenAll(t1, t2);
            foreach (string result in results)
            {
                Console.WriteLine(result);
            }
        }

        static async Task<string> GetInfoAsync(string name, int seconds)
        {
            //同一个工作线程
            //await Task.Delay(TimeSpan.FromSeconds(seconds));

            //不同工作线程
            await Task.Run(() =>
            {
                Thread.Sleep(seconds);
            });
            return $"Task {name} is running on a thread id{Thread.CurrentThread.ManagedThreadId}.Is" +
         $" thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}";
        }
        static void Main(string[] args)
        {
            //被线程池中同一个工作线程执行
            /*
             * 1从线程池中获取工作者线程，它将等待Task.Delay方法返回结果。然而，Task.Delay方法启动计时器指定一块代码
             * ，该代码会在计时器时间到了Task.Delay方法中指定描述后被调用。之后立即将工作者现成返回到线程池中。当计时器
             * 事件运行时，我们又从线程池中任意获取一个可用的工作者线程（可能就是运行第一个任务使用的线程）并运行计时器
             * 提供给他的代码
             */
            Task t=AsynchronousProcessing();
            t.Wait();
            Console.ReadKey();
        }
    }
}
