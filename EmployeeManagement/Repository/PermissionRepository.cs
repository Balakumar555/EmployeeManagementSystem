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
      
        public bool HasPermissionCheckAsync(int roleId, string activity, string feature)
        {
            activity = activity.ToLower().Trim();
            feature = feature.ToLower().Trim();

            var hasPermission= (from p in  _dbContext.permissions
                                join f in _dbContext.features  on p.FeatureId equals f.FeatureId
                                join a in _dbContext.activities on p.ActivityId equals a.ActivityId
                                where p.RoleId== roleId && f.Name.ToLower().Trim() == feature && a.Name == activity.ToLower().Trim() && p.IsEnabled==true
                                select p).Any();    

            return hasPermission;
        }
        public async Task<IEnumerable<Permission>> GetRolePermissionAsync(int roleId)
        {
            return await _dbContext.permissions.Where(x => x.RoleId == roleId).ToListAsync();
        }


        public async Task<Permission> InserOrUpdatePermissionsAsync(Permission permission)
        {
            if (permission != null)
            {
                if (permission.Id == 0)
                {
                    await _dbContext.AddAsync(permission);
                    await _dbContext.SaveChangesAsync();
                    return permission;
                }
                else
                {
                    var existingpermission = await _dbContext.permissions.FindAsync(permission.Id);
                    if (existingpermission != null)
                    {
                        existingpermission.RoleId = permission.RoleId;
                        existingpermission.ActivityId = permission.ActivityId;
                        existingpermission.FeatureId = permission.FeatureId;
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
