using CompanyApplication.Business.Services;
using CompanyApplication.Domain.Models;
using CompanyApplication.Utilities;

namespace CompanyApplication
{
    public class EmployeeController
    {
        private readonly EmployeeService _employeeService = new EmployeeService();
        private readonly DepartmentService _departmentService = new DepartmentService();

        public void CreateEmployee()
        {
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Name");
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Name can not be empty");
                name = Console.ReadLine();
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Surname");
            string surname = Console.ReadLine();
            while (string.IsNullOrEmpty(surname))
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Surname can not be empty");
                surname = Console.ReadLine();
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Age");
            int age = 0;
            bool isValidInput = false;
            while (!isValidInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out age))
                {
                    if ((age > 15) && (age < 100))
                    {
                        isValidInput = true;
                    }
                    else
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Enter a number between 16-99");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for age.");
                }
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Address");
            string address = Console.ReadLine();
            while (string.IsNullOrEmpty(address))
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Address can not be empty");
                address = Console.ReadLine();
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Department Name");
            string departmentName = Console.ReadLine();
            Employee newEmployee = new();
            newEmployee.Name = name;
            newEmployee.Surname = surname;
            newEmployee.Age = age;
            newEmployee.Address = address;
            newEmployee.Department = _departmentService.View(departmentName);
            if (_employeeService.Create(newEmployee, departmentName) is null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Something went wrong...");
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Green, "Employee was created successfully");
            }
        }
        public void ViewEmployeeSwitch()
        {
            Helper.ChangeTextColor(ConsoleColor.White, "Would you like to...\n" +
                    "1)See an employee with specific ID\n" +
                    "2)List all employees by name\n" +
                    "3)List all employees by age\n" +
                    "4)List all employees by address\n" +
                    "5)List all employees in a department\n" +
                    "6)List all employees\n" +
                    "7)See the number of all employees\n");
            int i = default;
            bool isValidSwitch = false;
            while (!isValidSwitch)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out i))
                {
                    isValidSwitch = true;
                }
                else
                {
                    Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid number for menu");
                }
            }
            switch (i)
            {
                case 1:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the ID:");
                    int id = default;
                    bool isValidInput = false;
                    while (!isValidInput)
                    {
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out id))
                        {
                            isValidInput = true;
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid number for ID");
                        }
                    }
                    var employeeByID = _employeeService.ViewById(id);
                    if (employeeByID is not null)
                    {
                        Console.WriteLine($"Employee ID: {employeeByID.Id}, Name: {employeeByID.Name}, Surname: {employeeByID.Surname}, Age: {employeeByID.Age}, Address: {employeeByID.Address}, Department: {employeeByID.Department.Name}");
                    }
                    else Helper.ChangeTextColor(ConsoleColor.Red, "No employee with such specification");
                    break;
                case 2:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the name or the surname:");
                    string name = Console.ReadLine();
                    while ((string.IsNullOrEmpty(name)) || (string.IsNullOrWhiteSpace(name)))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid name or surname");
                        name = Console.ReadLine();
                    }
                    if (_employeeService.ViewByName(name) is not null)
                    {
                        var allEmployeesByName = _employeeService.ViewByName(name);
                        foreach (var employee in allEmployeesByName)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    }
                    else
                    {
                        var allEmployeesByName = _employeeService.ViewBySurname(name);
                        foreach (var employee in allEmployeesByName)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    }
                    break;
                case 3:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the age:");
                    int age = default;
                    bool isValidAge = false;
                    while (!isValidAge)
                    {
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out age))
                        {
                            isValidAge = true;
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid number for age");
                        }
                    }
                    var employeeByAge = _employeeService.ViewByAge(age);
                    if (employeeByAge is not null)
                    {
                        foreach (var employee in employeeByAge)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    }
                    else Helper.ChangeTextColor(ConsoleColor.Red, "No employee with such specification");
                    break;
                case 4:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the address:");
                    string address = Console.ReadLine();
                    while (string.IsNullOrEmpty(address))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid address");
                        address = Console.ReadLine();
                    }
                    var employeeByAddress = _employeeService.ViewByAddress(address);
                    if (employeeByAddress is not null)
                    {
                        foreach (var employee in employeeByAddress)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    }
                    else Helper.ChangeTextColor(ConsoleColor.Red, "No employee with such specification");
                    break;
                case 5:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the department name:");
                    string department = Console.ReadLine();
                    while (string.IsNullOrEmpty(department))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid department name");
                        department = Console.ReadLine();
                    }
                    var employeeByDepartment =_employeeService.ViewByDepartment(department);
                    if (employeeByDepartment is not null)
                    {
                        foreach (var employee in employeeByDepartment)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    } 
                    else Helper.ChangeTextColor(ConsoleColor.Red,"No employee with such specification");
                    break;
                case 6:
                    var allEmployees = _employeeService.ViewAll();
                    if (allEmployees is not null)
                    {
                        foreach (var employee in allEmployees)
                        {
                            Console.WriteLine($"Employee ID: {employee.Id}, Name: {employee.Name}, Surname: {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, Department: {employee.Department.Name}");
                        }
                    }
                    else Helper.ChangeTextColor(ConsoleColor.Red, "Your employee list is currently empty");
                    break;
                case 7:
                    int count = _employeeService.ViewCount();
                    Console.WriteLine($"There are {count-1} employees in the database");
                    break;
            }

        }
        public void UpdateEmployeeSwitch()
        {
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Send an empty text if you don't want to change information. Enter the ID of the employee you want to edit");
            int id = 0;
            bool isValidInput = false;
            while (!isValidInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out id))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for ID.");
                }
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new name");
            string name = Console.ReadLine();
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new surname");
            string surname = Console.ReadLine();
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new age");
            int age = 0;
            bool isValidIAge = false;
            while (!isValidIAge)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out age))
                {
                    if ((age > 15) && (age < 100))
                    {
                        isValidInput = true;
                        break;
                    }
                    else
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Enter a number between 16-99");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for age.");
                }
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new address");
            string address = Console.ReadLine();
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new department name");
            string departmentName = Console.ReadLine();
            Employee newEmployee = _employeeService.ViewById(id);
            Department EmpDepart = _departmentService.View(departmentName);
            newEmployee.Name = newEmployee.Name ?? name;
            newEmployee.Surname = newEmployee.Surname ?? name;
            newEmployee.Age = age;
            newEmployee.Address = newEmployee.Address ?? address;
            newEmployee.Department = newEmployee.Department ?? EmpDepart;
            if (_employeeService.Update(id, newEmployee, departmentName) is null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Something went wrong");
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Green, "Employee was updated successfully");
            }
        }

        public void DeleteEmployeeSwitch()
        {
            Console.WriteLine("Enter the ID of the employee to delete:");
            int id = 0;
            bool isValidInput = false;
            while (!isValidInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out id))
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for ID.");
                }
            }
            Employee employeeToDelete = _employeeService.ViewById(id);

            if (employeeToDelete != null)
            {
                Console.WriteLine($"You are about to delete the  employee named '{employeeToDelete.Name} {employeeToDelete.Surname}'.");
                Console.Write("Are you sure you want to delete this employee? (Y/N): ");

                string confirmation = Console.ReadLine()?.ToUpper();
                if (confirmation == "Y")
                {
                    Employee deletedEmployee = _employeeService.Delete(employeeToDelete.Id);

                    if (deletedEmployee != null)
                    {
                        Console.WriteLine($"Employee '{deletedEmployee.Name} {deletedEmployee.Surname}' has been successfully deleted.");
                    }
                    else
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, $"Failed to delete the employee.");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion canceled by the user.");
                }
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Red, $"Employee with the name '{employeeToDelete.Name} {employeeToDelete.Surname}' was not found.");
            }
        }

    }
}
