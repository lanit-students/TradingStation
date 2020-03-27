using System;

namespace DataBaseService.Utils
{
    public class ExecutedScript
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
