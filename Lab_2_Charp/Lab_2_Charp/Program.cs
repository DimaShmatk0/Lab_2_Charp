using System.Diagnostics;

namespace Lab_2_Charp
{
    class Program
    {
        private static int[] GenerateArray(int length)
        {
            int[] arr = new int[length];
            var rand = new Random();
            for (int i = 0; i < length; i++)
            {
                arr[i] = rand.Next(1000);
            }
            arr[rand.Next(length)] = rand.Next(-2000, 0);
            return arr;
        }
        static void Main(string[] args)
        {
            int length = 1_000_000_000;
            int[] arr = GenerateArray(length);

            for (int t = 8; t <= 8; t *= 2)
            {
                var pmc = new ParallelMinController(arr, length, t);

                var stopwatch = Stopwatch.StartNew();
                pmc.RunController();
                stopwatch.Stop();
                Console.WriteLine($"Threads: {t} | Time: {stopwatch.Elapsed.TotalMilliseconds:F2} ms");
            }
        }
    }
}
