using DTO.RestRequests;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class AddBotCommand: IAddBotCommand
    {
        public Task<bool> Execute(CreateBotRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
