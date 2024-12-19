﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Table("Permission")]
    public class Permission : CommonLogs
    {
        [Key]
        public int PermissionId { get; set; }       
        public int? RoleId { get; set; }
        public int? ActivityId { get; set; }
        public bool IsEnabled { get; set; }
        public bool? IsDisabled { get; set; }
    }
}
