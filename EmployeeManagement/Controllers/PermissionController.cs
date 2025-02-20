﻿using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissions _permissions;
        public PermissionController(IPermissions permissions)
        {
            this._permissions = permissions;   
        }
        [HttpGet("GetRolePermissionAsync")]
        public async Task<ActionResult<Permission>> GetRolePermissionAsync(int roleId)
        {
            try
            {
                var datan = await _permissions.GetRolePermissionAsync(roleId);
                return Ok(datan);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("HasPermissionCheckAsync/{roleId}/{activity}/{feature}")]
        public ActionResult HasPermissionCheckAsync(int roleId, string activity, string feature)
        {
            
            bool hasPermission =  _permissions.HasPermissionCheckAsync(roleId, activity, feature);
           
            return Ok(hasPermission);
        }

        [HttpPost]
        [Route("InserOrUpdatePermissionsAsync")]
        public async Task<ActionResult<Permission>> InserOrUpdatePermissionsAsync(Permission permission)
        {
            try
            {
                var data = await _permissions.InserOrUpdatePermissionsAsync(permission);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
