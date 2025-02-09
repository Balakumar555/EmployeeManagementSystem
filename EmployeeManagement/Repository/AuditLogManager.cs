using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Repository_Configuration;

namespace EmployeeManagement.Repository
{
    public class AuditLogManager : IAuditLogManager
    {
        private readonly IRepository<AuditLog> _auditLogRepository;
        public AuditLogManager(IRepository<AuditLog> auditLogRepository)
        {
            this._auditLogRepository = auditLogRepository;    
        }
        public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync()
        {
           return await _auditLogRepository.GetAllAsync();
        }

        public async Task InsertOrUpdateAuditLogAsync(List<AuditLog> auditLog)
        {
            await _auditLogRepository.BulkInsertAsync(auditLog);
        }
    }
}