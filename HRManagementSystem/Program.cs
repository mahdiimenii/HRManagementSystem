using System;
using System.Collections.Generic;
using System.Linq;

namespace HRManagementSystem
{
    public enum Role
    {
        Intern,
        Junior,
        Senior,
        Manager,
        Director
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public Role EmployeeRole { get; set; }
        public Bank EmployeeBank { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Role: {EmployeeRole}, Salary: {Salary:C}, Bank: {EmployeeBank.BankName}, Account Number: {EmployeeBank.AccountNumber}";
        }
    }

    public class Bank
    {
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }

    public class HRManager
    {
        private List<Employee> employees = new List<Employee>();

        public void Hire(Employee employee)
        {
            employees.Add(employee);
        }

        public void Fire(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                employees.Remove(employee);
                Console.WriteLine($"{employee.Name} has been fired.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public void Promote(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null && employee.EmployeeRole < Role.Director)
            {
                employee.EmployeeRole += 1;
                Console.WriteLine($"{employee.Name} has been promoted to {employee.EmployeeRole}.");
            }
            else if (employee != null)
            {
                Console.WriteLine($"{employee.Name} is already at the highest position.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public void Demote(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee != null && employee.EmployeeRole > Role.Intern)
            {
                employee.EmployeeRole -= 1;
                Console.WriteLine($"{employee.Name} has been demoted to {employee.EmployeeRole}.");
            }
            else if (employee != null)
            {
                Console.WriteLine($"{employee.Name} is already at the lowest position.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        public void ListEmployees()
        {
            foreach (var employee in employees)
            {
                Console.WriteLine(employee);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hrManager = new HRManager();

            Console.WriteLine("HR Management System");
            while (true)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Hire Employee");
                Console.WriteLine("2. Fire Employee");
                Console.WriteLine("3. Promote Employee");
                Console.WriteLine("4. Demote Employee");
                Console.WriteLine("5. List Employees");
                Console.WriteLine("6. Exit");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter employee details:");
                        Console.Write("ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Salary: ");
                        decimal salary = decimal.Parse(Console.ReadLine());

                        Console.Write("Role (Intern, Junior, Senior, Manager, Director): ");
                        Role role = Enum.Parse<Role>(Console.ReadLine(), true);

                        Console.Write("Bank Name: ");
                        string bankName = Console.ReadLine();

                        Console.Write("Account Number: ");
                        string accountNumber = Console.ReadLine();

                        var employee = new Employee
                        {
                            Id = id,
                            Name = name,
                            Salary = salary,
                            EmployeeRole = role,
                            EmployeeBank = new Bank
                            {
                                BankName = bankName,
                                AccountNumber = accountNumber
                            }
                        };

                        hrManager.Hire(employee);
                        break;

                    case "2":
                        Console.Write("Enter employee ID to fire: ");
                        int fireId = int.Parse(Console.ReadLine());
                        hrManager.Fire(fireId);
                        break;

                    case "3":
                        Console.Write("Enter employee ID to promote: ");
                        int promoteId = int.Parse(Console.ReadLine());
                        hrManager.Promote(promoteId);
                        break;

                    case "4":
                        Console.Write("Enter employee ID to demote: ");
                        int demoteId = int.Parse(Console.ReadLine());
                        hrManager.Demote(demoteId);
                        break;

                    case "5":
                        hrManager.ListEmployees();
                        break;

                    case "6":
                        return;
                }
            }
        }
    }
}
