using CompanyApplication.Domain.Models;

namespace CompanyApplication.Business.Interfaces
{
    public interface IEmployee
    {
        Employee Create(Employee employee, string departmentName);
        Employee Delete(int id);
        Employee Update(int id, Employee employee, string departmentName);

        List<Employee> ViewAll();
        List<Employee> ViewByDepartment(int id);
        List<Employee> ViewByAge(int age);
        List<Employee> ViewByAddress(string address);
        Employee ViewById(int id);
        List<Employee> ViewByName(string name);
        List<Employee> ViewBySurname(string surname);
        int ViewCount();
    }
}
