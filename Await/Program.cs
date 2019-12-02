using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitResult
{
    class Program
    {
        static Task AsynchronyWithTPL()
        {
            Task<string> t = GetInfoAsync("Task 1");
            Task task2 = t.ContinueWith(task => Console.WriteLine(t.Result), TaskContinuationOptions.NotOnFaulted);
            Task task3 = t.ContinueWith(task => Console.WriteLine(t.Exception.InnerException), TaskContinuationOptions.OnlyOnFaulted);
            return Task.WhenAny(task2, task3);
        }

        static async Task AsynchronyWithAwait()
        {
            try
            {
                string result = await GetInfoAsync("Task 2");
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        static async Task<string> GetInfoAsync(string name)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            throw new Exception("Boom!!");
            return $"Task {name} is running on a thread id{Thread.CurrentThread.ManagedThreadId}.Is" +
                $" thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}";
        }
        static void Main(string[] args)
        {
            Task t = AsynchronyWithTPL();
            t.Wait();

            t = AsynchronyWithAwait();
            t.Wait();
            Console.ReadKey();
        }
    }
}
