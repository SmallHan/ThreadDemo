using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLazy
{
    class Program
    {
        static  void Main(string[] args)
        {
            var t =  ProcessAsynchronously();
            t.GetAwaiter().GetResult();
            Console.WriteLine("Hello World!");
        }
        static async Task ProcessAsynchronously()
        {
            var unsafeState = new UnsafeState();
            Task[] tasks = new Task[4];
            for (var i = 0; i < 4; i++)
            {
                tasks[i] = Task.Run(() => Worder(unsafeState));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("======华丽的分割线========");
            Console.ReadKey();
        }
        static void Worder(IHasValue state)
        {
            Console.WriteLine($"State value:{state.Value.Text}");
        }
        static ValueToAccess Compute()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            return new ValueToAccess($"{Thread.CurrentThread.ManagedThreadId}");
        }
        class UnsafeState : IHasValue
        {
            private ValueToAccess _value;
            public ValueToAccess Value => _value ?? (_value = Compute());
        }
        class ValueToAccess
        {
            private readonly string _text;
            public ValueToAccess(string text)
            {
                _text = text;
            }

            public string Text => _text;
        }
        interface IHasValue
        {
            ValueToAccess Value { get; }
        }
    }

}
