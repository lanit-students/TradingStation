using DTO.RestRequests;
using OperationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class RunBotCommand: IRunBotCommand
    {
        public Task<bool> Execute(EditBotRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
