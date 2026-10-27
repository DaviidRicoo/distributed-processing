using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EmployeeManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeManager employeeManager = new EmployeeManager();

            employeeManager.AddEmployee(new Employee(1, "John Smith", "IT", 5000));
            employeeManager.AddEmployee(new Employee(2, "Emma Brown", "HR", 4500));
            employeeManager.AddEmployee(new Employee(3, "Michael Wilson", "Finance", 6200));
            Console.WriteLine("All employees:");
            employeeManager.DisplayAllEmployees();

            string filePath = "employees.txt";
            employeeManager.SaveEmployeesToFile(filePath);

            Console.WriteLine("\nRemoving all employees from memory and loading from file...");
            employeeManager = new EmployeeManager();
            employeeManager.LoadEmployeesFromFile(filePath);
            Console.WriteLine("Employees loaded from file:");
            
            employeeManager.LoadStats();
        }
    }

    class Employee
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Department { get; private set; }
        public double Salary { get; private set; }

        public Employee(int id, string name, string department, double salary)
        {
            Id = id;
            Name = name;
            Department = department;
            Salary = salary;
        }
    }

    class EmployeeManager
    {
        private List<Employee> employees;

        public EmployeeManager()
        {
            employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void DisplayAllEmployees()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.Id}, Name: {employee.Name}, Department: {employee.Department}, Salary: {employee.Salary}");
            }
        }

        public void SaveEmployeesToFile(string filePath = "employees.txt")
        {
            if (employees.Count == 0) return;

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var employee in employees)
                    {
                        writer.WriteLine($"{employee.Id};{employee.Name};{employee.Department};{employee.Salary}"); // Formato actualizado[cite: 6]
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File read/write error: {ex.Message}");
            }
        }

        public void LoadEmployeesFromFile(string filePath = "employees.txt")
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Handle: Missing file.");
                return;
            }

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length == 4)
                        {
                            int id = Convert.ToInt32(parts[0]);
                            string name = parts[1];
                            string department = parts[2];
                            double salary = Convert.ToDouble(parts[3]);
                            employees.Add(new Employee(id, name, department, salary));
                        }
                        else
                        {
                            Console.WriteLine("Handle: Invalid data.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File read/write error: {ex.Message}");
            }
        }

        public void LoadStats()
        {
            if (employees.Count == 0) return;

            Console.WriteLine($"Employees loaded: {employees.Count}");
            Console.WriteLine($"Average salary: {Math.Round(employees.Average(e => e.Salary), 2)}");
            
            var topEmployee = employees.OrderByDescending(e => e.Salary).First();
            Console.WriteLine("Highest salary:");
            Console.WriteLine($"{topEmployee.Name} - {topEmployee.Salary}");
        }
    }
}