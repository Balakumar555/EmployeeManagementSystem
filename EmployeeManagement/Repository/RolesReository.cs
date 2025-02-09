using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class RolesReository : IRoles
    {
        private readonly ApplicationDBContext _DBContext;
       
        public RolesReository(ApplicationDBContext DBContext)
        {
            this._DBContext = DBContext;    
        }
        public async Task<Role> CreateUpdate(Role role)
        {
            if (role != null)
            {
                if (role.Id == 0)
                {
                    await _DBContext.AddAsync(role);
                    await _DBContext.SaveChangesAsync();
                    return role;
                }
                else
                {
                   var existingRole= await _DBContext.roles.FindAsync(role.Id);
                    if (existingRole != null)
                    {
                        existingRole.Name = role.Name;
                        existingRole.Description = role.Description;
                        existingRole.IsActive = role.IsActive;
                        existingRole.ModifiedBy = role.ModifiedBy;
                        existingRole.ModifiedOn = DateTimeOffset.Now;
                        await _DBContext.SaveChangesAsync();
                        return existingRole;
                    }
                }
            }
             return null;
        }

        public async Task<Role> GetByRoleId(int roleId)
        {
           var data= await _DBContext.roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (data == null)
            {
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }
            return data;
        }
    }
}
