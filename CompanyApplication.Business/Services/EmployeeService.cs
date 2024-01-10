using CompanyApplication.Business.Interfaces;
using CompanyApplication.DataContext.Repositories;
using CompanyApplication.Domain.Models;

namespace CompanyApplication.Business.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly DepartmentRepository _departmentRepository;
        private static int Count = 1;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _departmentRepository = new DepartmentRepository();
        }

        public Employee Create(Employee employee, string departmentName)
        {
            var existDepartment = _departmentRepository.View(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase));

            if (existDepartment == null)
            {
                return null;
            }
            int currentEmployeeCount = _employeeRepository.ViewAll(e => e.Department.Id == existDepartment.Id).Count;

            if (currentEmployeeCount >= existDepartment.Capacity)
            {
                return null;
            }

            employee.Department = existDepartment;
            employee.Id = Count;
            bool result = _employeeRepository.Create(employee);
            if (!result)
            {
                return null;
            }
            Count++;
            return employee;
        }

        public Employee Update(int id, Employee employee, string departmentName)
        {
            var existEmployee = _employeeRepository.View(e => e.Id == id);
            if (existEmployee is null) return null;

            var existDepartment = _departmentRepository.View(d => d.Name == departmentName);
            if (existDepartment is null) return null;

            if (!string.IsNullOrWhiteSpace(employee.Name))
            {
                existEmployee.Name = employee.Name;
            }

            if (!string.IsNullOrWhiteSpace(employee.Surname))
            {
                existEmployee.Surname = employee.Surname;
            }

            if (!string.IsNullOrWhiteSpace(employee.Address))
            {
                existEmployee.Address = employee.Address;
            }

            if (employee.Department != null && !string.IsNullOrWhiteSpace(employee.Department.Name))
            {
                existEmployee.Department = employee.Department;
            }

            if (employee.Age > 15 && employee.Age < 100)
            {
                existEmployee.Age = employee.Age;
            }
            return _employeeRepository.Update(existEmployee) ? existEmployee : null;
        }




        public List<Employee> ViewAll()
        {
            return _employeeRepository.ViewAll();
        }

        public List<Employee> ViewByAddress(string address)
        {
            return _employeeRepository.ViewAll(e=>e.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }

        public List<Employee> ViewByAge(int age)
        {
            return _employeeRepository.ViewAll(e => e.Age.Equals(age));
        }

        public List<Employee> ViewByDepartment(int id)
        {
            return (_employeeRepository.ViewAll(e => e.Department.Id.Equals(id)));
        }

        public List<Employee> ViewByDepartment(string name)
        {
            return (_employeeRepository.ViewAll(e => e.Department.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        public Employee ViewById(int id)
        {
            return _employeeRepository.View(e=>e.Id.Equals(id));
        }

        public List<Employee> ViewByName(string name)
        {
            return (_employeeRepository.ViewAll(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase)));
        }

        public List<Employee> ViewBySurname(string surname)
        {
            return (_employeeRepository.ViewAll(e => e.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase)));
        }

        public int ViewCount()
        {
            return Count;
        }
        public int ViewCountInDepartment(int departmentId)
        {
            return _employeeRepository.ViewAll(e => e.Department.Id == departmentId).Count;
        }

        public Employee Delete(int id)
        {
            var employeeToDelete = _employeeRepository.View(d => d.Id.Equals(id));

            if (employeeToDelete != null)
            {
                bool result = _employeeRepository.Delete(employeeToDelete);
                if (result)
                {
                    return employeeToDelete;
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
    }
}