using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoles _roles;
        public RoleController(IRoles roles)
        {
            this._roles = roles;
        }
        [HttpPost]
        [Route("CreateUpdateRoles")]
        public async Task<IActionResult> CreateUpdate(Role role)
        {
            var data = await _roles.CreateUpdate(role);
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetByRoleId(int roleId)
        {
            var data = await _roles.GetByRoleId(roleId);
            return Ok(data);    
        }
    }
}
