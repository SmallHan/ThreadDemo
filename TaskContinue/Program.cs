using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinue
{
    class Program
    {
        static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId} " +
                     $".Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return 42 * seconds;

        }
        static void Main(string[] args)
        {
            var firstTask = new Task<int>(() => TaskMethod("First Task", 3));
            var secondTask = new Task<int>(() => TaskMethod("Second Task", 2));

            firstTask.ContinueWith(t => Console.WriteLine($"The first is  {t.Result}" +
                $" is running on a thread id {Thread.CurrentThread.ManagedThreadId} " +
                     $".Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"),TaskContinuationOptions.OnlyOnRanToCompletion);

            firstTask.Start();
            secondTask.Start();

            Thread.Sleep(TimeSpan.FromSeconds(4));
            Task continuation= secondTask.ContinueWith(t => Console.WriteLine($"The Second is  {t.Result}" +
                $" is running on a thread id {Thread.CurrentThread.ManagedThreadId} " +
                     $".Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"), TaskContinuationOptions.OnlyOnRanToCompletion
                     | TaskContinuationOptions.ExecuteSynchronously);

            continuation.GetAwaiter().OnCompleted(() => Console.WriteLine($"Continuation Task Completed! Thread " +
                $"Id+{Thread.CurrentThread.ManagedThreadId },is thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}"));

            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
