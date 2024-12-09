namespace EmployeeManagement.Models
{
    public class UserInfirmation:CommonLogs
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
       
        public Guid? RoleId { get; set; }
        public string? RoleName { get; set; }
        public DateTimeOffset? LastPasswordChangedOn { get; set; }
        public bool? IsBlocked { get; set; }
       
    }
}
