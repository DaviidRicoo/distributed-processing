using System;
using System.Threading;

// Task 3 - Race Condition and Thread Synchronization
public class Program
{
    private const int ThreadCount = 20;
    private const int IncrementsPerThread = 100_000;

    // Shared resource
    private static int counter = 0;

    // Object used as the lock for Part B
    private static readonly object counterLock = new object();

    public static void Main()
    {
        int expected = ThreadCount * IncrementsPerThread;
        Console.WriteLine($"Threads: {ThreadCount}, increments per thread: {IncrementsPerThread:N0}");
        Console.WriteLine($"Expected value: {ThreadCount} x {IncrementsPerThread:N0} = {expected:N0}\n");

        // Part A - without synchronization
        Console.WriteLine("=== Part A - Race condition (no synchronization) ===");
        int unsafeResult = RunExperiment(IncrementUnsafe, "Unsafe");
        PrintComparison(unsafeResult, expected);

        // Part B - with lock
        Console.WriteLine("\n=== Part B - Synchronization with lock ===");
        int safeResult = RunExperiment(IncrementWithLock, "Locked");
        PrintComparison(safeResult, expected);
    }

    private static int RunExperiment(ThreadStart work, string namePrefix)
    {
        counter = 0;

        Thread[] threads = new Thread[ThreadCount];
        for (int i = 0; i < ThreadCount; i++)
        {
            threads[i] = new Thread(work) { Name = $"{namePrefix}-Worker-{i + 1}" };
        }

        // Start all threads
        foreach (Thread thread in threads)
        {
            thread.Start();
        }

        // Wait for all threads to finish
        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        return counter;
    }

    private static void IncrementUnsafe()
    {
        for (int i = 0; i < IncrementsPerThread; i++)
        {
            counter++; // read-modify-write: not atomic -> race condition
        }
    }

    private static void IncrementWithLock()
    {
        for (int i = 0; i < IncrementsPerThread; i++)
        {
            lock (counterLock)
            {
                counter++;
            }
        }
    }

    private static void PrintComparison(int obtained, int expected)
    {
        int lost = expected - obtained;
        Console.WriteLine($"Final counter value: {obtained:N0}");
        Console.WriteLine($"Expected value:      {expected:N0}");
        if (lost == 0)
        {
            Console.WriteLine("Result: CORRECT - the counter matches the expected value.");
        }
        else
        {
            Console.WriteLine($"Result: INCORRECT - {lost:N0} increments were lost ({100.0 * lost / expected:F2}%).");
        }
    }
}
