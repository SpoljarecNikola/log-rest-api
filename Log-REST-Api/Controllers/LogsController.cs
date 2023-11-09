using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Log_REST_Api.DataModel;
using Log_REST_Api.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Log_REST_Api.DatabaseContext;
using Log_REST_Api.Models;

namespace Log_REST_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogDatabaseContext _context;

        public LogsController(LogDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool LogExists(Guid id) => _context.Logs.Any(e => e.Id == id);

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogs()
        {
            return await _context.Logs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLog(Guid id)
        {
            var log = await _context.Logs.FindAsync(id);
            return log != null ? Ok(log) : NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLog(Guid id, Log log)
        {
            if (id != log.Id) return BadRequest("Log ID mismatch.");

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id)) return NotFound();
                else throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Log>> CreateLog(LogDTO logDto)
        {
            var log = new Log
            {
                Timestamp = logDto.Timestamp,
                Level = logDto.Level,
                AppId = logDto.AppId,
                Module = logDto.Module,
                Message = logDto.Message
            };

            await _context.Logs.AddAsync(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLog), new { id = log.Id }, log);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(Guid id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null) return NotFound();

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Log>>> SearchLogs(
                [FromQuery] DateTime? timeStamp,
                [FromQuery] LogEventLevel? level,
                [FromQuery] Guid? appId,
                [FromQuery] string? module
        )
        {
            var logsQuery = _context.Logs.AsQueryable();

            if (timeStamp.HasValue)
            {
                logsQuery = logsQuery.Where(log => log.Timestamp.Date == timeStamp.Value.Date);
            }

            if (level.HasValue)
            {
                logsQuery = logsQuery.Where(log => log.Level == level.Value);
            }

            if (appId.HasValue)
            {
                logsQuery = logsQuery.Where(log => log.AppId == appId.Value);
            }

            if (!string.IsNullOrWhiteSpace(module))
            {
                var lowerCaseModule = module.Trim().ToLower();
                logsQuery = logsQuery.Where(log => log.Module.ToLower().Contains(lowerCaseModule));
            }

            var logs = await logsQuery.ToListAsync();

            return logs.Any() ? Ok(logs) : NotFound("No logs found matching the search criteria.");
        }

    }
}
