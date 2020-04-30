using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    interface IAddBotCommand
    {
        Task<bool> Execute( request);
    }
}
