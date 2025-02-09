using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Role")]
    public class Role
    {
        public int Id { get; set; } 

        public string Name { get; set; } 

        public string Description { get; set; } 

        public Guid? CreatedBy { get; set; } 

        public DateTimeOffset? CreatedOn { get; set; } 

        public Guid? ModifiedBy { get; set; } 

        public DateTimeOffset? ModifiedOn { get; set; } 

        public bool? IsActive { get; set; } = true; // BIT DEFAULT 1
    }
}
