using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User MapUser(DbUser dbUser, string email);

        DbUser MapToDbUser(User user);

        UserCredential MapUserCredential(DbUserCredential dbUserCredential);

        DbUserCredential MapToDbUserCredential(UserCredential userCredential);
    }
}
