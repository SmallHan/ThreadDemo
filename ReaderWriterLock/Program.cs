using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterLock
{
    class Program
    {
        static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        static Dictionary<int, int> _item = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            var taskRead = new Task[3];
            for (var i = 1; i <= 3; i++)
            {
                taskRead[i - 1] = Task.Run(() =>
                {
                    Read();
                });
            }
            var taskWrite = new Task[2];
            for (var i = 1; i <= 2; i++)
            {
                string threadName = $"Thread:{i}";
                taskRead[i - 1] = Task.Run(() =>
                {
                    Write(threadName);
                });
            }
            Console.ReadKey();
        }

        static void Read()
        {
            while (true)
            {
                try
                {
                    _rw.EnterReadLock();
                    foreach (var key in _item.Keys)
                    {
                        Console.WriteLine($"key:{key}==>value:{_item[key]}");
                        Thread.Sleep(TimeSpan.FromSeconds(0.1));
                    }
                }
                finally
                {
                    _rw.ExitReadLock();
                }
            }
        }

        static void Write(string threadName)
        {
            while (true)
            {
                try
                {
                    int newKey = new Random().Next(500);
                    _rw.EnterUpgradeableReadLock();
                    if (!_item.ContainsKey(newKey))
                    {
                        try
                        {
                            _rw.EnterWriteLock();
                            _item[newKey] = 1;
                            Console.WriteLine($"New key {newKey} is add to a dictionary by a {threadName}");
                
                        }
                        finally
                        {
                            _rw.ExitWriteLock();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }
                finally
                {
                    _rw.ExitUpgradeableReadLock();
                }
            }
        }
    }
}
