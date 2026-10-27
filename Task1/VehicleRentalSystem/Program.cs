using System;
using System.Security.Cryptography.X509Certificates;

namespace VehicleRentalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            RentalCompany rentalCompany = new RentalCompany();

            rentalCompany.AddVehicle(new Vehicle("ABC123", "Toyota", 50));
            rentalCompany.AddVehicle(new Vehicle("XYZ789", "Honda", 60));
            rentalCompany.AddVehicle(new Vehicle("LMN456", "Ford", 55));

            rentalCompany.DisplayAvailableVehicles();

            Console.WriteLine("Enter the registration number of the vehicle you want to rent:");
            string regNumberToRent = Console.ReadLine();
            rentalCompany.RentVehicle(regNumberToRent);
  
           
            Console.WriteLine("Enter the number of days you want to rent the vehicle:");
            int rentalDays = Convert.ToInt32(Console.ReadLine());
            double cost = rentalCompany.GetRentalCost(regNumberToRent, rentalDays);
           Console.WriteLine($"The total rental cost for {rentalDays} days is: {cost}");
            
        }
    }

    class Vehicle
    {
        public string registrationNumber { get; private set; }
        public string brand { get; private set; }
        public double dailyRate { get; private set; }
        private bool isAvailable;

        public Vehicle(string registrationNumber, string brand, double dailyRate)
        {
            this.registrationNumber = registrationNumber;
            this.brand = brand;
            this.dailyRate = dailyRate;
            this.isAvailable = true;
        }

        public bool IsAvailable()
        {
            return isAvailable;
        }

        public void Rent()
        {
            isAvailable = false;
        }

        internal void Return()
        {
            isAvailable = true;
        }
    }

    class RentalCompany
    {
        private List<Vehicle> vehicles;

        public RentalCompany()
        {
            vehicles = new List<Vehicle>();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
        }

        public void DisplayAvailableVehicles()
        {
            Console.WriteLine("Available Vehicles:");
            foreach (var vehicle in vehicles)
            {
                if (vehicle.IsAvailable())
                {
                    Console.WriteLine($"Registration Number: {vehicle.registrationNumber}, Brand: {vehicle.brand}, Daily Rate: {vehicle.dailyRate}");
                }
            }
        }

        public void RentVehicle(string registrationNumber)
        {
            var vehicle = vehicles.Find(v => v.registrationNumber == registrationNumber);
            if (vehicle != null && vehicle.IsAvailable())
            {
                vehicle.Rent();
                Console.WriteLine($"Vehicle {registrationNumber} has been rented.");
            }
            else
            {
                Console.WriteLine($"Vehicle {registrationNumber} is not available for rent.");
            }
        }

        public void ReturnVehicle(string registrationNumber)
        {
            var vehicle = vehicles.Find(v => v.registrationNumber == registrationNumber);
            if (vehicle != null && !vehicle.IsAvailable())
            {
                vehicle.Return(); 
                Console.WriteLine($"Vehicle {registrationNumber} has been returned.");
            }
            else
            {
                Console.WriteLine($"Vehicle {registrationNumber} is not currently rented.");
            }
        }

        public double GetRentalCost(string registrationNumber, int rentalDays)
        {
            var vehicle = vehicles.Find(v => v.registrationNumber == registrationNumber);
            if (vehicle != null)
            {
                return vehicle.dailyRate * rentalDays;
            }
            else
            {
                Console.WriteLine($"Vehicle {registrationNumber} not found.");
                return 0;
            }
        }
}
}