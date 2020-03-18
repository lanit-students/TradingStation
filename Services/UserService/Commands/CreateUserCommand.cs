using DTO;
using UserService.Interfaces;
using Kernel;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUser<string, string>

    {
        public string Execute(string email, string password)
        {
            string message;

            if (CommonValidations.ValidateEmail(email))
            {
                var user = new User(email, password);
                message = createUserInDatabase(email, password);
            }
            else
            {
                message = "Invalid email";
            }
            return message;
        }
        private string createUserInDatabase(string email, string password)
        {
            return "User is already exist or User created";
        }

    }
}

