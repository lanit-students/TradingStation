using System.Threading.Tasks;
using DTO.RestRequests;

namespace UserService.Interfaces
{
    public interface IAddBotCommand
    {
        Task<bool> Execute(CreateBotRequest request);
    }
}
