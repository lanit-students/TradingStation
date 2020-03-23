using DTO;
using Kernel;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand

    {
        public string Execute(UserCredential userCredential)
        {
            CommonValidations.ValidateEmail(userCredential.Email);
            string message = createUser(userCredential);
            return message;
        }
        private string createUser(UserCredential userCredential)
        {
            return "User is already exist or User created";
        }

    }
}

