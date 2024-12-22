using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    public class BaseController : ControllerBase
    {
        protected int? CurrentUser
        {
            get
            {
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
                return !string.IsNullOrEmpty(userIdClaim?.Value) ? int.Parse(userIdClaim.Value) : (int?)null;
            }
        }

        protected int? CurrentUserRole
        {
            get
            {
                var roleClaim = User.Claims.FirstOrDefault(c => c.Type == "role");
                return !string.IsNullOrEmpty(roleClaim?.Value) ? int.Parse(roleClaim.Value) : null;
            }
        }
    }

}
