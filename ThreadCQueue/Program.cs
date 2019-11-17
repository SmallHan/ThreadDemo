using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadCQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = RunProgram();
            t.Wait();
            Console.WriteLine("ok");
            Console.ReadKey();
        }

        static async Task RunProgram()
        {
            var taskQueue = new ConcurrentQueue<CustomTask>();
            //生产
            var taskSource = Task.Run(() => TaskProducer(taskQueue));
            await taskSource;
            //消费者
            var processors = new Task[4];
            for (var i = 1; i <= 4; i++)
            {
                string processordId = i.ToString();
                processors[i - 1] = Task.Run(() => TaskProcessor(taskQueue, $"Processor {processordId}"));
            }
            await Task.WhenAll(processors);
        }
        static async Task TaskProducer(ConcurrentQueue<CustomTask> queue)
        {
            for (var i = 1; i <= 20; i++)
            {
                await Task.Delay(50);
                var workItem = new CustomTask { Id = i };
                queue.Enqueue(workItem);
            }
        }
        static async Task TaskProcessor(ConcurrentQueue<CustomTask> queue, string name)
        {
            CustomTask workItem;
            await GetRandomDelay();
            while (queue.TryDequeue(out workItem))
            {
                Console.WriteLine($"消费 {workItem.Id}===>{name}");
                await GetRandomDelay();
            }
        }

        static Task GetRandomDelay()
        {
            int delay = new Random(DateTime.Now.Millisecond).Next(1, 500);
            return Task.Delay(delay);
        }
    }
    class CustomTask
    {
        public int Id { get; set; }
    }
}
