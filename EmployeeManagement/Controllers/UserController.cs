using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            this._userManager = userManager;

        }
        [HttpPost]
        [Route("InsertOrUpdateUserAsync")]
        public async Task<IActionResult> InsertOrUpdateUserAsync([FromBody] UserRegistration user)
        {
            if (user == null)
            {
                return BadRequest("User data cannot be null.");
            }

            try
            {
                var result = await _userManager.InsertOrUpdateUserAsync(user);
                if (result == null)
                {
                    return StatusCode(500, "Failed to insert or update the user.");
                }

                return Ok(new
                {
                    Message = user.Id.HasValue ? "User updated successfully." : "User created successfully.",
                    User = result
                });
            }
            catch (Exception ex)
            {
                // Log the exception for debugging (use a logging framework in production)
                Console.Error.WriteLine(ex);

                // Return a generic error response
                return StatusCode(500, new
                {
                    Message = "An error occurred while processing the request.",
                    Details = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("FetchUsers")]
        public async Task<IActionResult> FetchUsersAsync()
        {
            try
            {
                var users = await _userManager.FetchUsersAsync();
                return Ok(users); // Return the list of users as JSON
            }
            catch (Exception ex)
            {
                // Handle and log exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
