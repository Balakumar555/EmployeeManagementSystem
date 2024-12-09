namespace EmployeeManagement.Models
{
    public class UserRegistration: CommonLogs
    {       
            public Guid? Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string Password { get; set; }           
            public Guid? RoleId { get; set; }
            public DateTimeOffset? LastPasswordChangedOn { get; set; }
            public bool? IsBlocked { get; set; }
           
        }
    
}
