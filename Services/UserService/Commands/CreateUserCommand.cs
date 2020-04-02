using DTO;
using Kernel;
using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IValidator<UserEmailPassword> validator;
        public CreateUserCommand([FromServices] IValidator<UserEmailPassword> validator)
        {
            this.validator = validator;
        }

        public void Execute(UserEmailPassword userEmailPassword)
        {
            validator.ValidateAndThrow(userEmailPassword);
            createUser(userEmailPassword);
        }

        private void createUser(UserEmailPassword userEmailPassword)
        {
            try
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

