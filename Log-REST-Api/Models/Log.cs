using Log_REST_Api.Enums;
using System.ComponentModel.DataAnnotations;

using Log_REST_Api.Interfaces;

namespace Log_REST_Api.DataModel
{
    
    public class Log : ILogEntry
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public LogEventLevel Level { get; set; }

        public Guid AppId { get; set; }
        public string Module { get; set; } = null!;

        public string Message { get; set; } = null!;
    }
}
