using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IDisableBotCommand
    {
        Task<bool> Execute(EditBotRequest request);
    }
}
