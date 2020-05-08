using Interfaces;
using System;
using System.Threading.Tasks;

namespace OperationService.Commands
{
    public class GetBotsCommand : ICommand<Guid, bool>
    {
        public async Task<bool> Execute(Guid id)
        {
            return true;
        }
    }
}
