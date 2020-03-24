using DTO;
using Kernel;
using System;
using System.Net;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand

    {
        public void Execute(UserCredential userCredential)
        {
            try
            {
                CommonValidations.ValidateEmail(userCredential.Email);
                createUser(userCredential);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void createUser(UserCredential userCredential)
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

