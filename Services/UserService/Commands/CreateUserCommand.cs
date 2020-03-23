using DTO;
using Kernel;
using System.Net;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand

    {
        public HttpStatusCode Execute(UserCredential userCredential)
        {
            CommonValidations.ValidateEmail(userCredential.Email);
            return createUser(userCredential);
        }

        private HttpStatusCode createUser(UserCredential userCredential)
        {
            return HttpStatusCode.Created;
        }

    }
}

