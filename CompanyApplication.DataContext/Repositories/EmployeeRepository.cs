using CompanyApplication.DataContext.Interfaces;
using CompanyApplication.Domain.Models;

namespace CompanyApplication.DataContext.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public bool Create(Employee entity)
        {
            try
            {
                DataBaseContext.Employees.Add(entity); return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Employee entity)
        {
            try
            {
                DataBaseContext.Employees.Remove(entity); return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Employee View(Predicate<Employee> filter)
        {
            return DataBaseContext.Employees.Find(filter);
        }

        public List<Employee> ViewAll(Predicate<Employee> filter = null)
        {
            return filter == null ? DataBaseContext.Employees : DataBaseContext.Employees.FindAll(filter);
        }

        public bool Update(Employee entity)
        {
            try
            {
                var existEmployee = View(s => s.Id == entity.Id);
                existEmployee = entity;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
