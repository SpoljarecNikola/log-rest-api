using Log_REST_Api.Enums;

namespace Log_REST_Api.Models
{
    public class LogDTO
    {
        public DateTime Timestamp { get; set; }
        public LogEventLevel Level { get; set; }
        public Guid AppId { get; set; }
        public string Module { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
