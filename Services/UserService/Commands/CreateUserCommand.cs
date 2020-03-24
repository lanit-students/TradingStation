using DTO;
using Kernel;
using System;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand

    {
        public void Execute(UserEmailPassword userEmailPassword)
        {
            try
            {
                CommonValidations.ValidateEmail(userEmailPassword.Email);
                createUser(userEmailPassword);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

