using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IRoles
    {
        Task<Role> CreateUpdate(Role role);       
        Task<Role> GetByRoleId(int roleId);

    }
}
