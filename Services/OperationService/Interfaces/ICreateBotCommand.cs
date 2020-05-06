using DTO.RestRequests;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface ICreateBotCommand
    {
        Task<bool> Execute(CreateBotRequest request);
    }
}
