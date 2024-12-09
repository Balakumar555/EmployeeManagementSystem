using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using EmployeeManagement.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeManagement.Repository
{
    public class UserDataManager : IUserManager
    {
       private readonly ApplicationDBContext _dbContext;
       

        public UserDataManager(ApplicationDBContext dbContext)
        {            
            this._dbContext = dbContext;
           
        }      

       
        public async Task<User> InsertOrUpdateUserAsync(UserRegistration userRegistration)
        {
            if (userRegistration == null)
                throw new ArgumentNullException(nameof(userRegistration), "User registration data cannot be null.");

            // Retrieve existing user if ID is provided; otherwise create a new one
            User user = userRegistration.Id.HasValue
                ? await _dbContext.users.FindAsync(userRegistration.Id.Value)
                : new User();

            if (user == null && userRegistration.Id.HasValue)
                throw new InvalidOperationException($"User with ID {userRegistration.Id.Value} does not exist.");

            // Map fields
            user.FirstName = userRegistration.FirstName;
            user.LastName = userRegistration.LastName;
            user.Email = userRegistration.Email;
            user.Phone = userRegistration.Phone;
            user.RoleId = userRegistration.RoleId;
            user.IsBlocked = userRegistration.IsBlocked;
            user.LastPasswordChangedOn = userRegistration.LastPasswordChangedOn ?? user.LastPasswordChangedOn;
            user.CreatedBy = userRegistration.CreatedBy ?? user.CreatedBy;
            user.CreatedOn = userRegistration.CreatedOn ?? user.CreatedOn ?? DateTimeOffset.UtcNow;
            user.ModifiedBy = userRegistration.ModifiedBy;
            user.ModifiedOn = DateTimeOffset.UtcNow;
            user.IsActive = userRegistration.IsActive;

            // Update password if provided
            if (!string.IsNullOrEmpty(userRegistration.Password))
            {
                var amplifyHashSalt = AmplifyHashSalt.GenerateSaltedHash(userRegistration.Password);
                user.PasswordHash = amplifyHashSalt.Hash;
                user.PasswordSalt = amplifyHashSalt.Salt;
                user.LastPasswordChangedOn = DateTimeOffset.UtcNow;
            }

            if (userRegistration.Id.HasValue)
            {
                // Update existing user
                _dbContext.users.Update(user);
            }
            else
            {
                // Insert new user
                await _dbContext.users.AddAsync(user);
            }

            // Save changes
            await _dbContext.SaveChangesAsync();

            // Return the user
            return user;
        }
        public async Task<IEnumerable<UserInfirmation>> FetchUsersAsync()
        {
            var users = await _dbContext.users
       .Select(user => new UserInfirmation
       {
           Id = user.Id,
           FirstName = user.FirstName,
           LastName = user.LastName,
           Email = user.Email,
           Phone = user.Phone,
           RoleId = user.RoleId,
           IsActive = user.IsActive,
           IsBlocked = user.IsBlocked,
           CreatedOn = user.CreatedOn,
           ModifiedOn = user.ModifiedOn
       })
       .ToListAsync();

            return users;
        }

        public Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
