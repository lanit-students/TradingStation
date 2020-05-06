using DTO.RestRequests;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IDisableBotCommand
    {
        Task<bool> Execute(DisableBotRequest request);
    }
}
