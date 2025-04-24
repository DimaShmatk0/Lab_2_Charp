using Lab_2_Charp;
using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        int length = 1_000_000_000;

        var stopwatch = Stopwatch.StartNew();
        int[] arr = GenerateArray(length);
        stopwatch.Stop();
        Console.WriteLine($"Time generate: {stopwatch.Elapsed.TotalMilliseconds:F2} ms");

        for (int countThread = 16; countThread <= 16; countThread *= 2)
        {
            var controller = new ParallelMinController(arr, length, countThread);
            stopwatch.Restart();
            controller.RunController();
            stopwatch.Stop();
            Console.WriteLine($"Threads: {countThread} | Time: {stopwatch.Elapsed.TotalMilliseconds:F2} ms");
        }
    }

    private static int[] GenerateArray(int length)
    {
        int[] arr = new int[length];
        Random rnd = new Random();
        for (int i = 0; i < length; i++)
        {
            arr[i] = rnd.Next(1000);
        }
        arr[166658519] = -1;
        return arr;
    }
}