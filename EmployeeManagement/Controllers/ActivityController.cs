using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivity _activity;
        public ActivityController(IActivity activity) 
        {
            this._activity = activity;
        } 
       

        [HttpPost]
        [Route("CreateUpdateActivity")]
        public async Task<ActionResult> CreateUpdate(Activity activity)
        {
            var data = await _activity.CreateUpdate(activity);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetActivityById")]
        public async Task<ActionResult> GetActivityById(int id)
        {
            var data = await _activity.GetActivityById(id);
            return Ok(data);
        }
        [HttpGet]
        [Route("GetActivities")]
        public async Task <ActionResult<Activity>> GetActivities()
        {
            var data = await _activity.GetActivities();
            return Ok(data);
        }
    }
}
