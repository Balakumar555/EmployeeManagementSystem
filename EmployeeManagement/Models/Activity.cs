using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Table("Activity")]
    public class Activity : CommonLogs
    {
        [Key]
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
