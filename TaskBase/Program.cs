using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBase
{
    class Program
    {
        static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }
        static int TaskMethod(string name)
        {
            Console.WriteLine($"Task {name} is running on a thread id {Thread.CurrentThread.ManagedThreadId} " +
                $".Is Thread pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }
        static void Main(string[] args)
        {
            TaskMethod("Main Thread Task");
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
            Console.WriteLine($"Result is {result}");

            task = CreateTask("Task2");
            //RunSynchronously该任务会运行在主线程中
            task.RunSynchronously();
            result = task.Result;
            Console.WriteLine($"Result is {result}");

            task = CreateTask("Task3");
            Console.WriteLine(task.Status);
            task.Start();
            //未完成
            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }
            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine($"Result is {result}");
            Console.ReadKey();
        }
    }
}
