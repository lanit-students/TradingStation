using DTO;
using Kernel;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand

    {
        public string Execute(UserCredential userCredential)
        {
            string message;

            if (CommonValidations.ValidateEmail(userCredential.Email))
            {
                message = createUser(userCredential);
            }
            else
            {
                message = "Invalid email";
            }
            return message;
        }
        private string createUser(UserCredential userCredential)
        {
            return "User is already exist or User created";
        }

    }
}

