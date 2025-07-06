namespace EmployeeManagement.Models
{
    public class EmployeeDTO : CommonLogs
    {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Designation { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Email { get; set; }
            public DateTimeOffset? DOJ { get; set; }
            public decimal? Salary { get; set; }
            public bool? Gender { get; set; }
    }
}
