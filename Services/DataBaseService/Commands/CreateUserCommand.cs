using DataBaseService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseService.Commands
{
    public class CreateUserCommand : ICommand<UserEmailPassword>
    {
        private readonly IRepository<UserEmailPassword> userRepository;

        public CreateUserCommand([FromServices] IRepository<UserEmailPassword> userRepository)
        {
            this.userRepository = userRepository;
        }
        public void Execute(UserEmailPassword data)
        {
            userRepository.Create(data);
        }
    }
}
