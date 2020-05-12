using System;
using System.Threading;

namespace Walterlv.Demo.Weak
{
    class Program
    {
        static void Main(string[] args)
        {
            new WeakReference<Foo>(new Foo());
            // NewObject();
            GCTest();
        }

        // private static object NewObject() => new WeakReference<F1>(new F1());

        private static void GCTest()
        {
            while (true)
            {
                Thread.Sleep(500);
                GC.Collect();
            }
        }
    }

    public class Foo
    {
        ~Foo()
        {
            Console.WriteLine("Foo is collected.");
        }
    }
}
