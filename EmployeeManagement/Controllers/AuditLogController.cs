using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogManager _auditLogManager;
        private readonly ILogger<AuditLogController> _logger;

        public AuditLogController(IAuditLogManager auditLogManager, ILogger<AuditLogController> logger)
        {
            this._auditLogManager = auditLogManager;
            this._logger = logger;
        }

        [HttpGet]
        [Route("GetAuditLogs")]
        public async Task<IActionResult> GetAuditLogsAsync()
        {
            _logger.LogInformation("Fetching audit logs.");
            var auditLogs = await _auditLogManager.GetAuditLogsAsync();
            return Ok(auditLogs);
        }

        [HttpPost]
        [Route("InsertOrUpdateAuditLog")]
        public async Task<IActionResult> InsertOrUpdateAuditLogAsync(List<AuditLog> auditLog)
        {
            if (auditLog == null)
            {
                _logger.LogWarning("Invalid request. Audit log data is required.");
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusMessage = "Invalid request. Audit log data is required."
                });
            }
            try
            {
                await _auditLogManager.InsertOrUpdateAuditLogAsync(auditLog);
                _logger.LogInformation("Audit log data inserted successfully.");
                return Ok(new
                {
                    StatusCode = StatusCodes.Status200OK,
                    StatusMessage = "Audit log data inserted successfully."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting or updating audit log data.");
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    StatusMessage = "Internal Server Error"
                });
            }
        }
    }
}
