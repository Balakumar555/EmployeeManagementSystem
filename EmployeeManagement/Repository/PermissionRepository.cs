using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Security;

namespace EmployeeManagement.Repository
{
    public class PermissionRepository : IPermissions
    {
        private readonly ApplicationDBContext _dbContext;
        public PermissionRepository(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
      
        public bool HasPermissionCheckAsync(int roleId, string activity)
        {
            activity = activity.ToLower().Trim();

            var hasPermission = _dbContext.permissions
                .Where(x => x.RoleId == roleId && x.IsEnabled)
                .Join(
                    _dbContext.activities,
                    permission => permission.ActivityId,
                    activityEntity => activityEntity.ActivityId,
                    (permission, activityEntity) => new { permission, activityEntity }
                )
                .Any(x => x.activityEntity.Name.ToLower().Trim() == activity);

            return hasPermission;
        }
        public async Task<IEnumerable<Permission>> GetRolePermissionAsync(int roleId)
        {
            //return await _dbContext.permissions.FindAsync(x => x.RoleId == roleId);
            return await _dbContext.permissions.Where(x => x.RoleId == roleId).ToListAsync();
        }


        public async Task<Permission> InserOrUpdatePermissionsAsync(Permission permission)
        {
            if (permission != null)
            {
                if (permission.PermissionId == 0)
                {
                    await _dbContext.AddAsync(permission);
                    await _dbContext.SaveChangesAsync();
                    return permission;
                }
                else
                {
                    var existingpermission = await _dbContext.permissions.FindAsync(permission.PermissionId);
                    if (existingpermission != null)
                    {
                        existingpermission.RoleId = permission.RoleId;
                        existingpermission.ActivityId = permission.ActivityId;
                        existingpermission.IsActive = permission.IsActive;
                        existingpermission.IsEnabled = permission.IsEnabled;
                        existingpermission.IsDisabled = permission.IsDisabled;
                        existingpermission.ModifiedBy = permission.ModifiedBy;
                        existingpermission.ModifiedOn = DateTimeOffset.Now;
                        await _dbContext.SaveChangesAsync();
                        return existingpermission;
                    }
                }
            }
            return null;
        }
    }
}
