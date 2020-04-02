using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;
using System;

namespace DataBaseService.Mappers
{
    public class UserCredentialMapper : IMapper<UserEmailPassword, DbUserCredential>
    {
        public DbUserCredential CreateMap(UserEmailPassword data)
        {
            return new DbUserCredential { Id = Guid.NewGuid(), Email = data.Email, PasswordHash = data.PasswordHash };
        }

        public UserEmailPassword CreateRemap(DbUserCredential data)
        {
            return new UserEmailPassword {Email = data.Email, PasswordHash = data.PasswordHash };
        }
    }
}
