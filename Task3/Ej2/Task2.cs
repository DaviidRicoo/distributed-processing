using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

// Task 2 - Parallel Download Simulation
public class Program
{
    private const int MonitorIntervalMs = 500;

    public static void Main()
    {
        string[] files = { "report.pdf", "photo.jpg", "music.mp3", "video.mp4", "dataset.csv" };
        int[] durationsMs = { 2000, 3000, 4000, 5000, 6000 };

        List<Thread> downloadThreads = new List<Thread>();
        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            int duration = durationsMs[i];
            Thread thread = new Thread(() => DownloadFile(file, duration))
            {
                Name = $"Download-{file}"
            };
            downloadThreads.Add(thread);
        }

        int totalSequentialMs = durationsMs.Sum();
        Stopwatch stopwatch = Stopwatch.StartNew();

        Console.WriteLine("Starting all downloads simultaneously...\n");
        foreach (Thread thread in downloadThreads)
        {
            thread.Start();
        }

        // Monitor the threads every 500 ms using IsAlive
        while (downloadThreads.Any(t => t.IsAlive))
        {
            Thread.Sleep(MonitorIntervalMs);
            List<string> active = downloadThreads.Where(t => t.IsAlive).Select(t => t.Name!).ToList();
            string activeText = active.Count > 0 ? string.Join(", ", active) : "none";
            Console.WriteLine($"[{stopwatch.Elapsed.TotalSeconds,5:F1} s] Active threads ({active.Count}): {activeText}");
        }

        // Make sure every download has really completed
        foreach (Thread thread in downloadThreads)
        {
            thread.Join();
        }
        stopwatch.Stop();

        Console.WriteLine("\nAll downloads completed.");
        Console.WriteLine($"Sum of individual download durations: {totalSequentialMs / 1000.0:F2} s");
        Console.WriteLine($"Total (concurrent) execution time:    {stopwatch.Elapsed.TotalSeconds:F2} s");
    }

    private static void DownloadFile(string fileName, int durationMs)
    {
        Console.WriteLine($"[{Thread.CurrentThread.Name}] Download of '{fileName}' started (expected {durationMs / 1000.0:F1} s).");
        Thread.Sleep(durationMs);
        Console.WriteLine($"[{Thread.CurrentThread.Name}] Download of '{fileName}' finished.");
    }
}
