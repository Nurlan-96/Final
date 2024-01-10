using CompanyApplication.Business.Interfaces;
using CompanyApplication.Business.Services;
using CompanyApplication.Domain.Models;
using CompanyApplication.Utilities;

namespace CompanyApplication
{
    public class DepartmentController 
    {
        private readonly DepartmentService _departmentService = new DepartmentService();
        private readonly EmployeeService _employeeService = new EmployeeService();

        public void CreateDepartment()
        {
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Name");
            string depname = Console.ReadLine();
            while (_departmentService.View(depname) is not null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, $"There is already a department called {depname}");
                depname = Console.ReadLine();
            }
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter Capacity");
            int capacity = 0;
            bool isValidCap = false;
            while (!isValidCap)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out capacity))
                {
                    isValidCap = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for capacity.");
                }
            }
            Department newDepartment = new();
            newDepartment.Capacity = capacity;
            newDepartment.Name = depname;
            if (_departmentService.Create(newDepartment, newDepartment.Name) is null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Something went wrong...");
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Green, "Department was created successfully");
            }
        }
        public void ViewDepartmentSwitch()
        {
            Helper.ChangeTextColor(ConsoleColor.White, "Would you like to...\n" +
                            "1) See a department with specific ID\n" +
                            "2) See a department with specific name\n" +
                            "3) List all departments by size\n" +
                            "4) List all departments\n" +
                            "5) See the number of departments\n");
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
                    var depById = _departmentService.View(id);
                    if (depById != null) Console.WriteLine($"Department ID: {depById.Id}, Name: {depById.Name}");
                    else Helper.ChangeTextColor(ConsoleColor.Red, "No department with that ID");
                    break;
                    case 2:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the department name:");
                    string name = Console.ReadLine();
                    while (string.IsNullOrEmpty(name))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid name");
                        name = Console.ReadLine();
                    }
                    var depByName = _departmentService.View(name);
                    if (depByName != null) Console.WriteLine($"Department ID: {depByName.Id}, Name: {depByName.Name}");
                    else Helper.ChangeTextColor(ConsoleColor.Red, "No department with that name");
                    break;
                    case 3:
                    Helper.ChangeTextColor(ConsoleColor.White, "Enter the size:");
                    int size = default;
                    bool isValidSize = false;
                    while (!isValidSize)
                    {
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out size))
                        {
                            isValidSize = true;
                        }
                        else
                        {
                            Helper.ChangeTextColor(ConsoleColor.Red, "Please enter a valid number for size");
                        }
                    }
                    var depBySize = _departmentService.ViewAll(size);
                    if (depBySize.Count.Equals(0))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "No department with that size");
                    }
                    else
                    {
                        foreach ( var dep in depBySize)
                        {
                            Console.WriteLine($"Department ID: {dep.Id}, Name: {dep.Name}.");
                        }
                    }
                    break;
                    case 4:
                    var allDeps = _departmentService.ViewAll();
                    if (allDeps.Count.Equals(0))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "Your department list is currently empty");
                    }
                    else
                    {
                        foreach (var department in allDeps)
                        {
                            Console.WriteLine($"Department ID: {department.Id}, Name: {department.Name}, Number of employees:{_departmentService.depSize(department)}.");
                        }
                    }
                    break;
                    case 5:
                    int depCount = _departmentService.ViewCount();
                    Console.WriteLine(depCount);
                    break;
            }
        }
        public void DeleteDepartment()
        {
            Console.WriteLine("Enter the name of the department to delete:");
            string departmentNameToDelete = Console.ReadLine();

            Department departmentToDelete = _departmentService.View(departmentNameToDelete);

            if (departmentToDelete != null)
            {
                int employeeCount = _employeeService.ViewCountInDepartment(departmentToDelete.Id);

                Console.WriteLine($"You are about to delete '{departmentToDelete.Name}' and its {employeeCount} employees.");
                Console.Write("Are you sure you want to delete this department and its employees? (Y/N): ");

                string confirmation = Console.ReadLine()?.ToUpper();
                if (confirmation == "Y")
                {
                    Department deletedDepartment = _departmentService.Delete(departmentToDelete.Id);
                    var employeeByDepartment = _employeeService.ViewByDepartment(departmentNameToDelete);
                    foreach (var employee in employeeByDepartment)
                    {
                        _employeeService.Delete(employee.Id);
                    }

                    if (deletedDepartment != null)
                    {
                        Console.WriteLine($"Department '{deletedDepartment.Name}' has been successfully deleted.");
                    }
                    else
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red,$"Failed to delete department '{departmentNameToDelete}'.");
                    }
                }
                else
                {
                    Console.WriteLine("Deletion canceled by the user.");
                }
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Red, $"Department with the name '{departmentNameToDelete}' was not found.");
            }
        }
        public void UpdateDepartment()
        {
            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Send an empty text if you don't want to change information. Enter the ID of the department you want to edit");
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

            while ((_departmentService.View(name) is not null) && (_departmentService.View(name).Id != id))
            {
                Helper.ChangeTextColor(ConsoleColor.Red, $"There is already a department called {name}");
                name = Console.ReadLine();
            }

            Helper.ChangeTextColor(ConsoleColor.DarkYellow, "Enter new capacity");
            int capacity = 0;
            bool isValidCap = false;
            while (!isValidCap)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out capacity))
                {
                    if (capacity < _departmentService.depSize(_departmentService.View(name)))
                    {
                        Helper.ChangeTextColor(ConsoleColor.Red, "The current number of employees in the department is higher than the capacity.\n" +
                            "Delete some employees or move them to a different department first");
                    }
                    else
                    {
                        isValidCap = true;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number for capacity.");
                }
            }
            Department newDepartment = _departmentService.View(id);
            newDepartment.Capacity = capacity;
            newDepartment.Name = string.IsNullOrWhiteSpace(name) ? newDepartment.Name : name;
            if (_departmentService.Update(id, newDepartment, capacity) is null)
            {
                Helper.ChangeTextColor(ConsoleColor.Red, "Something went wrong");
            }
            else
            {
                Helper.ChangeTextColor(ConsoleColor.Green, "Department was updated successfully");
            }
        }

    }
}