using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IPermissions
    {
        //Task<bool> IsGranted();
        //Task<bool> IsDenied();

        bool HasPermissionCheckAsync(int roleId, string Activity, string feature);
        Task<IEnumerable<Permission>> GetRolePermissionAsync(int roleId);
        Task<Permission> InserOrUpdatePermissionsAsync(Permission permission);

    }
}
