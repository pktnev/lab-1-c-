using System;
using System.Threading;

namespace ThreadSum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть крок: ");
            int step = int.Parse(Console.ReadLine());

            Console.WriteLine("Введiть кiлькiсть потокiв: ");
            byte numThreads = byte.Parse(Console.ReadLine());

            double[] sums = new double[numThreads];
            double[] counts = new double[numThreads];
            bool canStop = false;

            Thread[] threads = new Thread[numThreads];
            for (int i = 0; i < numThreads; i++)
            {
                int index = i;
                threads[i] = new Thread(() => Calculator(index, step, sums, counts, ref canStop));
                threads[i].Start();
            }

            Thread stopperThread = new Thread(() => Stopper(ref canStop));
            stopperThread.Start();

            stopperThread.Join();
            for (int i = 0; i < numThreads; i++)
            {
                Console.WriteLine($"Потiк {i + 1}: сума = {sums[i]}, кiлькiсть = {counts[i]}");
            }
        }

        static void Calculator(int index, int step, double[] sums, double[] counts, ref bool canStop)
        {
            double sum = 0;
            double count = 0;

            for (int i = 0; !canStop; i += step)
            {
                sum += i;
                count++;
            }

            sums[index] = sum;
            counts[index] = count;
        }

        static void Stopper(ref bool canStop)
        {
            Thread.Sleep(10000);
            canStop = true;
        }
    }
}
