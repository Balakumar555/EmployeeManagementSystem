using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("AuditLog")]
    public class AuditLog: CommonLogs
    {
        [Key]
        public Guid AuditLogId { get; set; }
        public string? EntityName { get; set; }
        public string? ColumnName { get; set; }
        public string? Description { get; set; }
        public string? ValueBefore { get; set; }
        public string? ValueAfter { get; set; }

    }
}
