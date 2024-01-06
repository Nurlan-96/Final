using CompanyApplication.DataContext.Interfaces;
using CompanyApplication.Domain.Models;

namespace CompanyApplication.DataContext.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        public bool Create(Department entity)
        {
            try
            {
                DataBaseContext.Departments.Add(entity); return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(Department entity)
        {
            try
            {
                DataBaseContext.Departments.Remove(entity); return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Department View(Predicate<Department> filter)
        {
            return DataBaseContext.Departments.Find(filter);
        }

        public List<Department> ViewAll(Predicate<Department> filter = null)
        {
            return filter == null ? DataBaseContext.Departments : DataBaseContext.Departments.FindAll(filter);
        }

        public bool Update(Department entity)
        {
            try
            {
                var existDepartment = View(s => s.Id == entity.Id);
                existDepartment = entity;
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
