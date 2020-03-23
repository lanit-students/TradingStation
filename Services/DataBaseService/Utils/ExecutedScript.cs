using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Utils
{
    class ExecutedScript
    {
        public DateTime ExecutionTime { get; }
        public string ExecutedCode { get; }

        public ExecutedScript(DateTime executionTime, string executedCode)
        {
            ExecutionTime = executionTime;
            ExecutedCode = executedCode;
        }
    }
}
