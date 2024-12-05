using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : IEmployee
    {
        ApplicationDBContext _DbContext;
        public EmployeeRepository(ApplicationDBContext DbContext)
        {
            this._DbContext = DbContext;
        }
        public async Task<Employee> CreateOrUpdate(Employee employee)
        {
            if (employee != null)
            {
                if (employee.Id == 0)
                {

                    await _DbContext.employees.AddAsync(employee);
                    await _DbContext.SaveChangesAsync();
                    return employee;
                }
                else
                {
                    var existingEmployee = await _DbContext.employees.FindAsync(employee.Id);
                    if (existingEmployee != null)
                    {
                        existingEmployee.Name = employee.Name;
                        existingEmployee.Designation = employee.Designation;
                        await _DbContext.SaveChangesAsync();
                        return existingEmployee;
                    }
                }
            }
            return null;
        }

        public async Task<bool> Delete(int employeeId)
        {
            // Find the employee by ID
            var employee = await _DbContext.employees
                                            .FirstOrDefaultAsync(e => e.Id == employeeId);

            // If no employee is found, return false
            if (employee == null)
            {
                return false;
            }

            // Remove the employee from the DbSet
            _DbContext.employees.Remove(employee);

            // Save changes to the database
            await _DbContext.SaveChangesAsync();

            // Return true if the employee was successfully deleted
            return true;
        }

        public async Task<Employee> GetById(int employeeId)
        {
            var employee = await _DbContext.employees
                                    .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
            }

            return employee;
        }

        public  Task<List<Employee>> SearchEmployee(string searchInput)
        {
            // Normalize input
            string normalizedInput = searchInput.ToLower().Trim();

            // Query the database for matching employees
            return  _DbContext.employees
                .Where(x =>
                    x.Name.ToLower().Contains(normalizedInput) ||   // Check if Name contains the input
                    x.Designation.ToLower().Contains(normalizedInput) ||  // Check if Designation contains the input
                    x.Id.ToString().Contains(normalizedInput))  // Check if Id contains the input
                .ToListAsync();  // Materialize the result
        }


        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var data = await _DbContext.employees.ToListAsync();
            return data;
        }

        public Task<bool> VerifyEmployee()
        {
            throw new NotImplementedException();
        }

    }
}
