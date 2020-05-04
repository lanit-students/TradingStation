using Microsoft.Extensions.Logging;
using System;

namespace LogReader.Models
{
    public class Log
    {
        public Guid Id { get; set; }

        public Guid? ParentId { get; set; }

        public LogLevel Level { get; set; }

        public DateTime Time { get; set; }

        public string Message { get; set; }

        public string ServiceName { get; set; }
    }
}
