using System;
using System.Threading;

// Task 1 - Basic Thread Creation and Coordination
public class Program
{
    private const int ArraySize = 100;

    private static int[] numbers = Array.Empty<int>();

    // Results written by the worker threads (each thread writes only its own fields)
    private static long sum;
    private static int min;
    private static int max;
    private static double average;

    public static void Main()
    {
        GenerateNumbers();
        PrintNumbers();

        Thread sumThread = new Thread(CalculateSum) { Name = "SumCalculatorThread" };
        Thread minMaxThread = new Thread(CalculateMinMax) { Name = "MinMaxCalculatorThread" };
        Thread averageThread = new Thread(CalculateAverage) { Name = "AverageCalculatorThread" };

        Console.WriteLine("\nStarting worker threads...");
        sumThread.Start();
        minMaxThread.Start();
        averageThread.Start();

        // Main thread waits until all worker threads have finished
        sumThread.Join();
        minMaxThread.Join();
        averageThread.Join();
        Console.WriteLine("All worker threads have finished.\n");

        Console.WriteLine("=== Results ===");
        Console.WriteLine($"Sum:     {sum}");
        Console.WriteLine($"Minimum: {min}");
        Console.WriteLine($"Maximum: {max}");
        Console.WriteLine($"Average: {average:F2}");
    }

    private static void GenerateNumbers()
    {
        Random random = new Random();
        numbers = new int[ArraySize];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 1001);
        }
    }

    private static void PrintNumbers()
    {
        Console.WriteLine($"Generated array of {ArraySize} random integers:");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write($"{numbers[i],5}");
            if ((i + 1) % 10 == 0) Console.WriteLine();
        }
    }

    private static void CalculateSum()
    {
        Console.WriteLine($"[{Thread.CurrentThread.Name}] started.");
        long localSum = 0;
        foreach (int n in numbers)
        {
            localSum += n;
        }
        sum = localSum;
        Console.WriteLine($"[{Thread.CurrentThread.Name}] finished.");
    }

    private static void CalculateMinMax()
    {
        Console.WriteLine($"[{Thread.CurrentThread.Name}] started.");
        int localMin = int.MaxValue;
        int localMax = int.MinValue;
        foreach (int n in numbers)
        {
            if (n < localMin) localMin = n;
            if (n > localMax) localMax = n;
        }
        min = localMin;
        max = localMax;
        Console.WriteLine($"[{Thread.CurrentThread.Name}] finished.");
    }

    private static void CalculateAverage()
    {
        Console.WriteLine($"[{Thread.CurrentThread.Name}] started.");
        long localSum = 0;
        foreach (int n in numbers)
        {
            localSum += n;
        }
        average = (double)localSum / numbers.Length;
        Console.WriteLine($"[{Thread.CurrentThread.Name}] finished.");
    }
}
