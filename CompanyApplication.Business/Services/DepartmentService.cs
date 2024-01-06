using CompanyApplication.Business.Interfaces;
using CompanyApplication.DataContext.Repositories;
using CompanyApplication.Domain.Models;

namespace CompanyApplication.Business.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;
        private static int Count = 1;
        public DepartmentService()
        {
            _employeeRepository = new EmployeeRepository();
            _departmentRepository = new DepartmentRepository();
            _employeeService = new EmployeeService();
        }

        public Department Create(Department department, string departmentName)
        {
            var existDepartment = _departmentRepository.View(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase));
            if (existDepartment != null) return null;
            department.Id = Count;
            bool result = _departmentRepository.Create(department);
            if (!result) return null;
            Count++;
            return department;
        }

        public Department Delete(string name)
        {
            var departmentToDelete = _departmentRepository.View(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (departmentToDelete != null)
            {
                bool result = _departmentRepository.Delete(departmentToDelete);
                if (result)
                {
                    return departmentToDelete;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Department Delete(int id)
        {
            var departmentToDelete = _departmentRepository.View(d => d.Id.Equals(id));

            if (departmentToDelete != null)
            {
                bool result = _departmentRepository.Delete(departmentToDelete);
                if (result)
                {
                    return departmentToDelete;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Department Update(int id, Department department, int capacity)
        {
            var existDepartment = _departmentRepository.View(d => d.Id == id);
            if (existDepartment is null) return null;
            existDepartment.Name = department.Name ?? department.Name;
            existDepartment.Capacity = department.Capacity;
            return _departmentRepository.Update(existDepartment) ? existDepartment : null;
        }

        public Department View(int id)
        {
            return _departmentRepository.View(d => d.Id.Equals(id));
        }

        public Department View(string name)
        {
            return _departmentRepository.View(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Department> ViewAll()
        {
            return _departmentRepository.ViewAll();
        }

        public List<Department> ViewAll(int size)
        {
            return _departmentRepository.ViewAll().Where(d => _employeeRepository.ViewAll(e => e.Department.Id == d.Id).Count == size)
                .ToList();
        }

        public int depSize(Department department)
        {
            return _employeeService.ViewCountInDepartment(department.Id);
        }

        public int ViewCount()
        {
            return _departmentRepository.ViewAll().Count;
        }
    }
}
