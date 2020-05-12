using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Walterlv.Demo.Weak
{
    class Program
    {
        static void Main(string[] args)
        {
            var reference = new WeakReference<TaskCompletionSource<object>>(new F1());
            //var task0 = Task.Run(() => Test1(reference));
            var task1 = Task.Run(() => Test2(reference));
            Task.WaitAll(task1);
        }

        private static async void Test1(WeakReference<TaskCompletionSource<object>> reference)
        {
            reference.TryGetTarget(out var target);
            Test(target.Task);
        }

        private static async void Test(Task task)
        {
            while (true)
            {
                await task;
            }
        }

        private static async Task Test2(WeakReference<TaskCompletionSource<object>> reference)
        {
            while (true)
            {
                await Task.Delay(500).ConfigureAwait(false);
                GC.Collect();
                await Task.Delay(500).ConfigureAwait(false);
                PrintReferrence(reference);
            }
        }

        private static void PrintReferrence(WeakReference<TaskCompletionSource<object>> reference)
        {
            var has = reference.TryGetTarget(out _);
            Console.WriteLine(has);
        }
    }

    public class F1 : TaskCompletionSource<object>
    {
        ~F1()
        {
            Console.WriteLine("F1");
        }
    }
}
