using Log_REST_Api.Enums;

namespace Log_REST_Api.Interfaces
{
    public interface ILogEntry
    {
        DateTime Timestamp { get; set; }
        LogEventLevel Level { get; set; }
        Guid AppId { get; set; }
        string Module { get; set; }
        string Message { get; set; }
    }
}
