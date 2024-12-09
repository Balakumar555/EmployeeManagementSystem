using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Repository
{
    public class AuthenticationDataManager : IAuthenticationManager
    {
        private readonly ApplicationDBContext _dbContext;
        private string _tokenKey = string.Empty;
        public AuthenticationDataManager(ApplicationDBContext dbContext, IConfiguration configuration)
        {
            this._dbContext = dbContext;
            var jwtSection = configuration.GetSection("JWT");
            _tokenKey = jwtSection["tokenKey"].ToString();
        }
        public async Task<AuthResponse> AuthenticateUserAsync(UserAuthentication authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication), "Authentication data cannot be null.");

            var response = new AuthResponse();

            // Fetch the user by username (assuming username maps to Email or another unique identifier)
            var user = await _dbContext.users.SingleOrDefaultAsync(u => u.Email == authentication.username || u.Phone == authentication.username);

            if (user == null)
            {
                response.ValidUser = false;
                response.ValidPassword = false;
                response.StatusCode = StatusCodes.Status404NotFound; // Use StatusCodes for clarity
                response.StatusMessage = "User  not found.";
                return response;
            }

            // Check if the user is active
            if (!user.IsActive)
            {
                response.ValidUser = true;
                response.ValidPassword = false;
                response.IsActive = false;
                response.StatusCode = StatusCodes.Status403Forbidden; // Forbidden
                response.StatusMessage = "User  is inactive.";
                return response;
            }

            // Validate password
            bool isPasswordValid = PasswordGenerateHashSalt.VerifyPassword(authentication.password, user.PasswordHash, user.PasswordSalt);
            if (!isPasswordValid)
            {
                response.ValidUser = true;
                response.ValidPassword = false;
                response.IsActive = true;
                response.StatusCode = StatusCodes.Status401Unauthorized; // Unauthorized
                response.StatusMessage = "Invalid password.";
                return response;
            }

            // Generate JWT Token if the password is valid
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_tokenKey);

            if (tokenKey.Length == 0)
            {
                throw new Exception("JWT token key is not configured properly.");
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Email, user.Email), // Use user.Email or user.Id as needed
            //new Claim(ClaimTypes.Role, user.RoleId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var writtenToken = tokenHandler.WriteToken(token);

            // Successful response
            response = new AuthResponse
            {
                IsActive = true,
                JwtToken = writtenToken,
                StatusCode = StatusCodes.Status200OK, // OK
                StatusMessage = "Valid user.",
                ValidPassword = true,
                ValidUser = true
            };

            return response;
        }


        public async Task<ApplicationUser> GenarateUserClaimsAsync(AuthResponse auth)
        {
            try
            {
                ApplicationUser applicationUser = null;

                var toeknKey = Encoding.ASCII.GetBytes(_tokenKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principle = tokenHandler.ValidateToken(auth.JwtToken,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(toeknKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    }, out securityToken);
                var jwtTken = securityToken as JwtSecurityToken;

                if (jwtTken != null && jwtTken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                {
                    string username = principle.Identity.Name;

                    var user = await _dbContext.users.SingleOrDefaultAsync(x => x.Email.ToLower().Trim() == username.ToLower().Trim() || x.Phone.Trim() == username.Trim());
                    if (user != null)
                    {
                        applicationUser = new ApplicationUser()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            FullName = user.FirstName + "" + user.LastName,
                            Phone = user.Phone,
                            RoleId = user.RoleId,

                        };
                    }

                }
                return applicationUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
