using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }       
        //public string PhoneNumber { get; set; }
        //public string Email { get; set; }
        //public DateTime DOJ { get; set; }
        //public DateTime DOB { get; set; }
        //public decimal Salary { get; set; }

    }
}
