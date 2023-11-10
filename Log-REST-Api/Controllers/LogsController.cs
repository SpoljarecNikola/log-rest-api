using Microsoft.AspNetCore.Mvc;
using Log_REST_Api.DataModel;
using Log_REST_Api.Enums;
using Log_REST_Api.Models;
using Log_REST_Api.Services;

namespace Log_REST_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(Guid id)
        {
            var log = await _logService.GetLog(id);
            return log != null ? Ok(log) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLog(Guid id, Log log)
        {
            var success = await _logService.UpdateLog(id, log);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Log>> CreateLog(LogDTO logDto)
        {
            var log = await _logService.CreateLog(logDto);
            return CreatedAtAction(nameof(GetLog), new { id = log.Id }, log);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(Guid id)
        {
            var success = await _logService.DeleteLog(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Log>>> SearchLogs(
            [FromQuery] DateTime? timeStamp,
            [FromQuery] LogEventLevel? level,
            [FromQuery] Guid? appId,
            [FromQuery] string? module)
        {
            var logs = await _logService.SearchLogs(timeStamp, level, appId, module);
            return logs.Any() ? Ok(logs) : NotFound("No logs found matching the search criteria.");
        }
    }

}
