using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class ActivityRepository : IActivity
    {
        private readonly ApplicationDBContext _dbContext;
        public ActivityRepository(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext; 
        }
        public async Task<Activity> CreateUpdate(Activity activity)
        {
            if (activity != null)
            {
                if (activity.ActivityId == 0)
                {

                    await _dbContext.activities.AddAsync(activity);
                    await _dbContext.SaveChangesAsync();
                    return activity;
                }
                else
                {
                    var existingactivity = await _dbContext.activities.FindAsync(activity.ActivityId);
                    if (existingactivity != null)
                    {
                        existingactivity.Name = activity.Name;
                        existingactivity.Code = activity.Code;                       
                        existingactivity.IsActive = activity.IsActive;
                        existingactivity.ModifiedBy = activity.ModifiedBy;
                        existingactivity.ModifiedOn = DateTimeOffset.Now;
                        await _dbContext.SaveChangesAsync();
                        return existingactivity;
                    }
                }
            }
            return null;
        }

        public async Task<List<Activity>> GetActivities()
        {
            return await _dbContext.activities.ToListAsync();
        }

        public async Task<Activity> GetActivityById(int id)
        {
            var data= await _dbContext.activities.FirstOrDefaultAsync(x=> x.ActivityId==id);
            if (data == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return data;
        }
    }
}
