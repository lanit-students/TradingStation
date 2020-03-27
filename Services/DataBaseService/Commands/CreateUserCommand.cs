using DataBaseService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
