using System;
using System.Collections.Generic;

namespace ElectricityConsumption
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the kWh consumed day 1 of week:");
            double day1 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 2 of week:");
            double day2 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 3 of week:");
            double day3 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 4 of week:");
            double day4 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 5 of week:");
            double day5 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 6 of week:");
            double day6 = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter the kWh consumed day 7 of week:");
            double day7 = Convert.ToDouble(Console.ReadLine());

            double totalConsumption = day1 + day2 + day3 + day4 + day5 + day6 + day7;
            double averageConsumption = totalConsumption / 7;
            double[] dailyConsumptions = { day1, day2, day3, day4, day5, day6, day7 };
            double highestConsumption = 0;
            double lowestConsumption = double.MaxValue;
            int dayHighest = 0;
            int dayLowest = 0;

            Console.WriteLine("Daily electricity consumption:");
            for (int i = 0; i < dailyConsumptions.Length; i++)
            {
                Console.WriteLine($"Day {i + 1}: {dailyConsumptions[i]} kWh");

                if (dailyConsumptions[i] > highestConsumption)
                {
                    highestConsumption = dailyConsumptions[i];
                    dayHighest = i + 1;
                }

                if (dailyConsumptions[i] < lowestConsumption)
                {
                    lowestConsumption = dailyConsumptions[i];
                    dayLowest = i + 1;
                }
            }

            Console.WriteLine($"Total consumption: {totalConsumption} kWh");
            Console.WriteLine($"Average consumption: {averageConsumption} kWh");
            Console.WriteLine($"Highest consumption: Day {dayHighest} - {highestConsumption} kWh");
            Console.WriteLine($"Lowest consumption: Day {dayLowest} - {lowestConsumption} kWh");
            Console.WriteLine("Days with above average consumption:");

            for(int i = 0; i < dailyConsumptions.Length; i++)
            {
                if (dailyConsumptions[i] > averageConsumption)
                {
                    Console.WriteLine($"Day {i + 1}");
                }
            }

        }
    }
}