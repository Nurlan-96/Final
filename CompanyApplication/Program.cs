using CompanyApplication;
using CompanyApplication.Utilities;
EmployeeController employeeController = new EmployeeController();
DepartmentController departmentController = new DepartmentController();

while (true)
{
    Helper.ChangeTextColor(ConsoleColor.Green, "Company Application");
    Helper.ChangeTextColor(ConsoleColor.White, "Choose one of the options below:\n" +
        "1)Create\n" +
        "2)View\n" +
        "3)Update\n" +
        "4)Delete\n");
    string menu = Console.ReadLine();
    bool result = int.TryParse(menu, out int Menu);
    if (result && Menu > 0 && Menu < 5)
    {
        switch (Menu)
        {
            case (int)Helper.Menus.Create:
                Helper.ChangeTextColor(ConsoleColor.White, "What do you want to create?\n" +
                "1)Employee\n" +
                "2)Department\n");
                string menu1 = Console.ReadLine();
                bool result1 = int.TryParse(menu1, out int CreateMenu);

                if (result1 && CreateMenu > 0 && CreateMenu < 3)
                {
                    switch (CreateMenu)
                    {
                        case 1:
                        employeeController.CreateEmployee();
                        break;

                        case 2:
                        departmentController.CreateDepartment();
                        break;
                    }
                    break;
                }
                break;

            case (int)Helper.Menus.View:
                Helper.ChangeTextColor(ConsoleColor.White, "What do you want to view?\n" +
                    "1)Employees\n" +
                    "2)Departments\n");
                string menu2 = Console.ReadLine();
                bool result2 = int.TryParse(menu2, out int ViewMenu);

                if (result2 && ViewMenu > 0 && ViewMenu < 3)
                {
                    switch (ViewMenu)
                    {
                        case 1:
                        employeeController.ViewEmployeeSwitch();
                        break;
                        case 2:
                        departmentController.ViewDepartmentSwitch();
                        break;
                    }
                    break;
                }
                break;

            case (int)Helper.Menus.Update:
                Helper.ChangeTextColor(ConsoleColor.White, "What do you want to update?\n" +
                        "1)Employee\n" +
                        "2)Department\n");

                string menu3 = Console.ReadLine();
                bool result3 = int.TryParse(menu3, out int UpdateMenu);

                if (result3 && UpdateMenu > 0 && UpdateMenu < 3)
                {
                    switch (UpdateMenu)
                    {
                        case 1:
                            employeeController.UpdateEmployeeSwitch();
                            break;

                        case 2:
                            departmentController.UpdateDepartment();
                            break;
                    }
                    break;
                }
                break;

            case (int)Helper.Menus.Delete:
                Helper.ChangeTextColor(ConsoleColor.White, "What do you want to delete?\n" +
                "1)Employee\n" +
                "2)Department\n");

                string menu4 = Console.ReadLine();
                bool result4 = int.TryParse(menu4, out int DeleteMenu);

                if (result4 && DeleteMenu > 0 && DeleteMenu < 3)
                {
                    switch (DeleteMenu)
                    {
                        case 1:
                            employeeController.DeleteEmployeeSwitch();
                            break;

                        case 2:
                        departmentController.DeleteDepartment();
                        break;
                    }
                    break;
                }
                break;
        }
    }
    else if (Menu == 0)
    {
        Helper.ChangeTextColor(ConsoleColor.Blue, "Closing the application");
    }
    else
    {
        Helper.ChangeTextColor(ConsoleColor.Red, "Invalid input");
    }
}