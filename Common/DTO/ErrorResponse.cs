using System;

namespace DTO
{
    public class ErrorResponse : OperationResult
    {
        public DateTime UtcTime { get; set; }
        public string Header { get; set; }
        public string Message { get; set; }
    }
}
