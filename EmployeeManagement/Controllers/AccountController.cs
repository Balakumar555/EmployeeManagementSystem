using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;
        public AccountController(IAuthenticationManager authenticationManager)
        {
            this._authenticationManager = authenticationManager;
        }
        [HttpPost]
        [Route("AuthenticateUserAsync")]
        public async Task<IActionResult> AuthenticateUserAsync(UserAuthentication authentication)
        {
            if (authentication == null)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusMessage = "Invalid request. Authentication data is required."
                });
            }

            try
            {
                var authResponse = await _authenticationManager.AuthenticateUserAsync(authentication);

                if (!authResponse.ValidUser || !authResponse.ValidPassword)
                {
                    return Unauthorized(new
                    {
                        authResponse.StatusCode,
                        authResponse.StatusMessage
                    });
                }

                if (!authResponse.IsActive)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new
                    {
                        authResponse.StatusCode,
                        authResponse.StatusMessage
                    });
                }

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    StatusMessage = "Internal Server Error",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("GenerateUserClaimsAsync")]
        public async Task<IActionResult> GenerateUserClaimsAsync(AuthResponse authentication)
        {
            if (authentication == null)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusMessage = "Invalid request. Authentication data is required."
                });
            }

            try
            {
                var authResponse = await _authenticationManager.GenarateUserClaimsAsync(authentication);

                if (authResponse == null)
                {
                    return NotFound(new
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        StatusMessage = "User claims could not be generated."
                    });
                }

                var response = new
                {
                    Success = true,
                    StatusCode = StatusCodes.Status200OK,
                    StatusMessage = "OK",
                    Data = authResponse,
                    Message = "Authentication successful"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var exceptionResponse = new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    StatusMessage = "Internal Server Error",
                    Error = ex.Message
                };

                return StatusCode(StatusCodes.Status500InternalServerError, exceptionResponse);
            }
        }

    }

}
