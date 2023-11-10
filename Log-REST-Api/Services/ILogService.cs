using Log_REST_Api.DataModel;
using Log_REST_Api.Enums;
using Log_REST_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Log_REST_Api.Services
{
    public interface ILogService
    {
        Task<Log> GetLog(Guid id);
        Task<Log> CreateLog(LogDTO logDto);
        Task<bool> UpdateLog(Guid id, Log log);

        Task<bool> DeleteLog(Guid id);

        Task<IEnumerable<Log>> SearchLogs(
               [FromQuery] DateTime? timeStamp,
               [FromQuery] LogEventLevel? level,
               [FromQuery] Guid? appId,
               [FromQuery] string? module
       );

    }
}
