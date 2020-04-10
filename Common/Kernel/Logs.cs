using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel
{
    class Logs
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public string Message { get; set; }
        public string ServiceName { get; set; }
    }
}
