using DTO;
using Kernel;
using System;

namespace UserService.Commands
{
    public class CreateUserCommand 

    {
        public string Execute(string email, string password)
        {
            string message;

            if (CommonValidations.ValidateEmail(email))
            {
                var user = new User(email, password, Guid.Empty);
                message = sendNewUserInDataBaseService(email, password);
            }
            else
            {
                message = "Invalid email";
            }
            return message;
        }
        private string sendNewUserInDataBaseService(string email, string password)
        {
            return "User is already exist or User created";
        }

    }
}

