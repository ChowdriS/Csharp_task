using System;

namespace SampleApp
{

    public delegate void ThresholdReachedHandler(int count);


    public class Counter
    {
        private int _count = 0;
        private readonly int _threshold;


        public event ThresholdReachedHandler? ThresholdReached;

        public Counter(int threshold)
        {
            _threshold = threshold;
        }

        public void Increment()
        {
            _count++;
            Console.WriteLine($"Current Count: {_count}");

            if (_count == _threshold)
            {

                ThresholdReached?.Invoke(_count);
            }
        }
    }

    class Program
    {

        static void alert(int count)
        {
            Console.WriteLine($"\n-Alert: Counter reached {count}!");
        }

        static void log(int count)
        {
            Console.WriteLine($"\n-Log: Event triggered at count {count}.");
        }

        static void Main(string[] args)
        {

            Counter counter = new Counter(5);


            counter.ThresholdReached += alert;
            counter.ThresholdReached += log;

            Console.WriteLine("Press any key to increment the counter...");
            for (int i = 0; i < 10; i++)
            {
                Console.ReadKey();
                counter.Increment();
            }

            Console.WriteLine("Program completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}