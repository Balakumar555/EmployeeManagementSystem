using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IUserManager
    {
        Task<User> InsertOrUpdateUserAsync(UserRegistration user);

        Task<IEnumerable<UserInfirmation>> FetchUsersAsync();

        Task<ApplicationUser> GetCurrentUserAsync(string email);
    }
}
