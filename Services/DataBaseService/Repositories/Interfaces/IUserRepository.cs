using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);

        void CreateUserCredential(UserCredential userCredential);

        UserCredential GetUserCredential(string email);

        void DeleteUser(Guid userIdCredential);
    }
}
