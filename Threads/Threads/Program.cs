using System;
using System.Threading;

namespace Threads
{
    class Printer
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void Stop()
        {
            cancellationTokenSource.Cancel();
        }

        public void PrintCyan()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                Thread.Sleep(100);
                Console.WriteLine("cyan color");
            }
        }

        public void PrintGrey()
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                Thread.Sleep(200);
                Console.WriteLine("grey color");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Printer p = new Printer();

            Thread thread1 = new Thread(p.PrintCyan);
            Thread thread2 = new Thread(p.PrintGrey);

            thread1.Start();
            thread2.Start();

            Console.ReadKey();

            p.Stop();

            thread1.Join();
            thread2.Join();

            Thread.Sleep(1000);
        }
    }
}
