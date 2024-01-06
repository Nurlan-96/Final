using CompanyApplication.Domain.Models;

namespace CompanyApplication.Business.Interfaces
{
    public interface IDepartment
    {
        Department Create(Department department, string departmentName);
        Department Update(int id, Department department, int capacity);
        Department Delete(int id);
        Department Delete(string name);
        List<Department> ViewAll();
        List<Department> ViewAll(int size);
        Department View(int id);
        Department View(string name);
        int ViewCount();
    }
}
