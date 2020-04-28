using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperationService.BotLogic
{
    interface IBot
    {
        void Run() { }

        void Disable() { }

        void GetStatus() { }
    }
}
