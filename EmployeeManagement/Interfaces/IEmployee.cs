using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployee
    {
        Task<Employee> CreateOrUpdate(Employee employee);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetById(int employeeId);
        Task<List<Employee>> SearchEmployee(string Search);
        Task<bool> Delete(int employeeId);
        Task<bool> VerifyEmployee();
        Task<Employee> GetEmployeeDetailsByEmail(string email);
    }
}