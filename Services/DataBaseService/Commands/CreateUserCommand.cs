using DataBaseService.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

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
            //TODO Change to custom exception
            if (string.IsNullOrEmpty(data.Email) || string.IsNullOrEmpty(data.PasswordHash))
            {
                throw new Exception("Not correct data");
            }
            userRepository.Create(data);
        }
    }
}
