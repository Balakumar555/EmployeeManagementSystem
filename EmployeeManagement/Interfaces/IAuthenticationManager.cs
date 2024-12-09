using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IAuthenticationManager
    {
        Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication);
        Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth);
    }
}
