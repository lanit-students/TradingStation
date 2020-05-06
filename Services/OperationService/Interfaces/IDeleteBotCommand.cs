using DTO.RestRequests;
using System.Threading.Tasks;

namespace OperationService.Interfaces
{
    public interface IDeleteBotCommand
    {
        Task<bool> Execute(DeleteBotRequest request);
    }
}
