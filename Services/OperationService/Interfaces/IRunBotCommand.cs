using DTO.RestRequests;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IRunBotCommand
    {
        Task<bool> Execute(RunBotRequest request);
    }
}
