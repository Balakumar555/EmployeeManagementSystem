using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IActivity
    {
        Task<Activity> CreateUpdate(Activity ctivity);
        Task<Activity> GetActivityById(int id);
        Task <List<Activity>> GetActivities();
    }
}
