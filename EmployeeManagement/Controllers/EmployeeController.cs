using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {

        private readonly IEmployee _employee;
        private readonly IPermissions _permissions;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployee employee, IPermissions permissions, ILogger<EmployeeController> logger)
        {

            _employee = employee;
            _permissions = permissions;
            _logger = logger;
        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<ActionResult<Employee>> CreateOrUpdate(Employee employee)
        {
            try
            {
                _logger.LogInformation("CreateOrUpdate method started.");

                var data = await _employee.CreateOrUpdate(employee);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating/updating employee.");
                throw;
            }

        }
        [HttpGet]
        [Route("GetById/{employeeId}")]
        public async Task<ActionResult<Employee>> GetById(int employeeId)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            }

            if (_permissions.HasPermissionCheckAsync(1, "Get", "EmployeeController"))
            {
                return Forbid(); // Return 403 Forbidden if the user does not have permission
            }
            try
            {
                _logger.LogInformation("GetById method started.");
                var data = await _employee.GetById(employeeId);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee details.");
                throw;
            }

        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<ActionResult<Employee>> GetEmployees()
        {
            try
            {
                _logger.LogInformation("GetEmployees method started.");
                var data = await _employee.GetEmployees();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving employee details.");
                throw;
            }

        }
        [HttpGet]
        [Route("SearchEmployees")]
        public async Task<ActionResult<Employee>> SearchEmployee(string searchInput)
        {
            try
            {
                var data = await _employee.SearchEmployee(searchInput);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        //
        [HttpDelete]
        [Route("DeleteByEmployeeId")]
        public async Task<ActionResult<bool>> Delete(int employeeId)
        {
            try
            {
                if (employeeId == null) // or any other condition to check for null
                {
                    return BadRequest("Invalid employee ID");
                }
                var data = await _employee.Delete(employeeId);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("GetEmployeeDetailsByEmail/{email}")]
        public async Task<ActionResult<Employee>> GetEmployeeDetailsByEmailAsync(string email)
        {
            try
            {
                var data = await _employee.GetEmployeeDetailsByEmail(email);
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
   
}
