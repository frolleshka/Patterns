using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Patterns
{
    class Program
    {
        private static ConcurrentBag<int> buffer = new ConcurrentBag<int>();
        static async Task Main(string[] args)
        {
            var currentId = Thread.CurrentThread.ManagedThreadId;

            await Foo(IsAsyncRunning.Async);
            Foo(IsAsyncRunning.Sync).GetAwaiter().GetResult();

            Console.WriteLine(buffer.Contains(currentId));
        }



        
        public static async Task Foo(IsAsyncRunning isSync)
        {
            var curent = Thread.CurrentThread.ManagedThreadId;
            for (int i = 0; i < 100; i++)
            {
                var query = 0;

                await isSync.Run(GetIntSync, GetInt);
                if(curent != Thread.CurrentThread.ManagedThreadId)
                {
                    var dddd = 0;
                }
            }

        }

        public static async Task GetInt()
        {
            if (!buffer.Contains(Thread.CurrentThread.ManagedThreadId))
            {
                buffer.Add(Thread.CurrentThread.ManagedThreadId);
            }
            await Task.Delay(100);
            //return 42;
        }

        public static void GetIntSync()
        {
            if (!buffer.Contains(Thread.CurrentThread.ManagedThreadId))
            {
                buffer.Add(Thread.CurrentThread.ManagedThreadId);
            }
            
            //return 666;
        }
    }
    public struct IsAsyncRunning
    {
        public bool IsAsync;
        public static IsAsyncRunning Sync => new IsAsyncRunning { IsAsync = false };
        public static IsAsyncRunning Async => new IsAsyncRunning { IsAsync = true };
    }

    public static class IsAsyncRunningExtension
    {
        public static async Task<T> Run<T>(this IsAsyncRunning isAsyncRunning, Func<T> sync, Func<Task<T>> asyncFunc)
        {
            return isAsyncRunning.IsAsync ? await asyncFunc() : sync();
        }

        public static async Task Run(this IsAsyncRunning isAsyncRunning, Action sync, Func<Task> asyncFunc)
        {
            if (isAsyncRunning.IsAsync) await asyncFunc(); else sync();
        }
    }
}
