using SwiftLogger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftLogger.Models
{
    public class LogEvent
    {
        public DateTime Timestamp { get; set; }
        public LogLevel Level { get; set; }
        public string? Message { get; set; }
    }
}
