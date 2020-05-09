using DTO.RestRequests;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetBotsCommand : ICommand<Guid, List<BotInfoRequest>>
    {
        public async Task<List<BotInfoRequest>> Execute(Guid id)
        {
            return new List<BotInfoRequest>();
        }
    }
}
