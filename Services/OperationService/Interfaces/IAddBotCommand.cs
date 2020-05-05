using DTO.RestRequests;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IAddBotCommand
    {
        Task<bool> Execute(CreateBotRequest request);
    }
}
