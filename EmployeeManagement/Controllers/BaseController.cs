using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    public class BaseController : ControllerBase
    {
        protected Guid? CurrentUser
        {
            get
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                return !string.IsNullOrEmpty(userIdClaim?.Value) ? Guid.Parse(userIdClaim.Value) : (Guid?)null;
            }
        }

        protected Guid? CurrentUserRole
        {
            get
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
                return !string.IsNullOrEmpty(roleClaim?.Value) ? Guid.Parse(roleClaim.Value) : null;
            }
        }
    }

}
