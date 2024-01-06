using CompanyApplication.Domain.Models;

namespace CompanyApplication.DataContext
{
    public static class DataBaseContext
    {
        public static List<Department> Departments { get; set; }
        public static List<Employee> Employees { get; set; }

        static DataBaseContext()
        {
            Departments = new List<Department>();
            Employees = new List<Employee>();
        }
    }
}
