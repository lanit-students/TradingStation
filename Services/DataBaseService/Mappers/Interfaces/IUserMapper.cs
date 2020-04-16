using DataBaseService.Database.Models;
using DTO;

namespace DataBaseService.Mappers.Interfaces
{
    public interface IUserMapper
    {
        User MapUser(DbUser dbUser, string email);

        DbUser MapToDbUser(User user);

        UserAvatar MapUserAvatar(DbUsersAvatars userAvatar);

        DbUsersAvatars MapToDbUserAvatar(UserAvatar userAvatar);
        
        UserCredential MapUserCredential(DbUserCredential dbUserCredential);

        DbUserCredential MapToDbUserCredential(UserCredential userCredential);
    }
}
