using EmployeeManagement.DB_Configuration;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployee _employee;
        public EmployeeController(IEmployee employee)
        {

            _employee = employee;

        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        public async Task<ActionResult<Employee>> CreateOrUpdate(Employee employee)
        {
            try
            {
                var data = await _employee.CreateOrUpdate(employee);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        public async Task<ActionResult<Employee>> GetById(int employeeId)
        {
            try
            {
                var data = await _employee.GetById(employeeId);
                return data;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        [HttpGet]
        [Route("GetEmployees")]
        public async Task<ActionResult<Employee>> GetEmployees()
        {
            try
            {
                var data = await _employee.GetEmployees();
                return Ok(data);
            }
            catch (Exception ex)
            {
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


    }
}
