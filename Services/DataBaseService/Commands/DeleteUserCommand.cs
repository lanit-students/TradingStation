using DataBaseService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DataBaseService.Commands
{
    public class DeleteUserCommand : ICommand<Guid>
    {
        private readonly IRepository<UserEmailPassword> userRepository;

        public DeleteUserCommand([FromServices] IRepository<UserEmailPassword> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Execute(Guid data)
        {
            userRepository.Delete(data);
        }
    }
}
