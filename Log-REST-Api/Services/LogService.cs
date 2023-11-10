using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Log_REST_Api.DataModel;
using Log_REST_Api.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Log_REST_Api.DatabaseContext;
using Log_REST_Api.Models;

namespace Log_REST_Api.Services
{
    public class LogService : ILogService
    {
        private readonly LogDatabaseContext _context;

        public LogService(LogDatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private bool LogExists(Guid id) => _context.Logs.Any(e => e.Id == id);

        public async Task<Log> GetLog(Guid id)
        {
            return await _context.Logs.FindAsync(id);
        }

        public async Task<Log> CreateLog(LogDTO logDto)
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
            return log;
        }

        public async Task<bool> UpdateLog(Guid id, Log log)
        {
            if (id != log.Id)
                return false;

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogExists(id))
                    return false;
                else
                    throw;
            }
        }

        public async Task<bool> DeleteLog(Guid id)
        {
            var log = await _context.Logs.FindAsync(id);
            if (log == null)
                return false;

            _context.Logs.Remove(log);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Log>> SearchLogs(DateTime? timeStamp, LogEventLevel? level, Guid? appId, string? module)
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

            return await logsQuery.ToListAsync();
        }
    }
}
