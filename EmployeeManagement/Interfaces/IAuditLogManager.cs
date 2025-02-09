using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IAuditLogManager
    {
        Task<IEnumerable<AuditLog>> GetAuditLogsAsync();
        Task InsertOrUpdateAuditLogAsync(List<AuditLog> auditLog);
    }
}
