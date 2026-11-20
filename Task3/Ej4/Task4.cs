using System;
using System.Diagnostics;
using System.Threading;

// Task 4 - Parallel Matrix Processing
public class Program
{
    private const int Size = 6000;        // rows = columns (>= 1000, increased for measurable times)
    private const int ThreadCount = 4;
    private const int Repetitions = 5;    // each measurement is averaged over several runs

    public static void Main()
    {
        Console.WriteLine($"Generating two {Size} x {Size} integer matrices...");
        int[][] a = GenerateMatrix(Size, seed: 1);
        int[][] b = GenerateMatrix(Size, seed: 2);
        int[][] sequentialResult = CreateMatrix(Size);
        int[][] parallelResult = CreateMatrix(Size);

        // Warm-up (JIT compilation, page faults) so it does not distort the measurements
        AddSequential(a, b, sequentialResult);
        AddParallel(a, b, parallelResult, ThreadCount);

        double sequentialMs = Measure(() => AddSequential(a, b, sequentialResult));
        double parallelMs = Measure(() => AddParallel(a, b, parallelResult, ThreadCount));

        bool equal = AreEqual(sequentialResult, parallelResult);
        double speedup = sequentialMs / parallelMs;

        Console.WriteLine($"Threads used in parallel version: {ThreadCount}");
        Console.WriteLine($"Average over {Repetitions} runs\n");
        Console.WriteLine($"Sequential execution time: {sequentialMs:F2} ms");
        Console.WriteLine($"Parallel execution time:   {parallelMs:F2} ms");
        Console.WriteLine($"Speedup (T_seq / T_par):   {speedup:F2}x");
        Console.WriteLine($"Results identical:         {(equal ? "YES" : "NO")}");
    }

    private static int[][] CreateMatrix(int size)
    {
        int[][] matrix = new int[size][];
        for (int i = 0; i < size; i++)
        {
            matrix[i] = new int[size];
        }
        return matrix;
    }

    private static int[][] GenerateMatrix(int size, int seed)
    {
        Random random = new Random(seed);
        int[][] matrix = CreateMatrix(size);
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i][j] = random.Next(0, 1000);
            }
        }
        return matrix;
    }

    private static double Measure(Action action)
    {
        Stopwatch stopwatch = new Stopwatch();
        for (int r = 0; r < Repetitions; r++)
        {
            stopwatch.Start();
            action();
            stopwatch.Stop();
        }
        return stopwatch.Elapsed.TotalMilliseconds / Repetitions;
    }

    private static void AddRows(int[][] a, int[][] b, int[][] result, int startRow, int endRow)
    {
        for (int i = startRow; i < endRow; i++)
        {
            int[] rowA = a[i];
            int[] rowB = b[i];
            int[] rowR = result[i];
            for (int j = 0; j < rowR.Length; j++)
            {
                rowR[j] = rowA[j] + rowB[j];
            }
        }
    }

    private static void AddSequential(int[][] a, int[][] b, int[][] result)
    {
        AddRows(a, b, result, 0, result.Length);
    }

    private static void AddParallel(int[][] a, int[][] b, int[][] result, int threadCount)
    {
        int rows = result.Length;
        int rowsPerThread = rows / threadCount;
        Thread[] threads = new Thread[threadCount];

        for (int t = 0; t < threadCount; t++)
        {
            int startRow = t * rowsPerThread;
            // The last thread also takes the remaining rows if not evenly divisible
            int endRow = (t == threadCount - 1) ? rows : startRow + rowsPerThread;
            threads[t] = new Thread(() => AddRows(a, b, result, startRow, endRow))
            {
                Name = $"MatrixWorker-{t + 1} [rows {startRow}-{endRow - 1}]"
            };
            threads[t].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }

    private static bool AreEqual(int[][] x, int[][] y)
    {
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x[i].Length; j++)
            {
                if (x[i][j] != y[i][j]) return false;
            }
        }
        return true;
    }
}
