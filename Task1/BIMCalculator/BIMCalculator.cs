using System;

namespace BMICalculator
{
    class BIMCalculator
    {
        static void Main(string[] args)
        {
            Console.Write("Enter weight (kg): ");
            double weight = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter height (m): ");
            double height = Convert.ToDouble(Console.ReadLine());

            double bmi = weight / (height * height);

            string category = "";
            if (bmi < 18.5)
            {
                category = "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.99)
            {
                category = "Normal weight";
            }
            else if (bmi >= 25 && bmi <= 29.99)
            {
                category = "Overweight";
            }
            else if (bmi >= 30)
            {
                category = "Obesity";
            }

            Console.WriteLine($"BMI = {Math.Round(bmi, 2)}");
            Console.WriteLine($"Category: {category}");
        }
    }
}