using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    [Table("Feature")]
    public class Feature
    {
        [Key]
        public int FeatureId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? CreatedOn { get; set; }

        public Guid? ModifiedBy { get; set; }

        public DateTimeOffset? ModifiedOn { get; set; }

        public bool IsActive { get; set; }
    }
}
