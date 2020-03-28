using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;
using System;

namespace DataBaseService.Mappers
{
    public class UserMapper : IMapper<UserEmailPassword, DbUser>
    {
        public DbUser CreateMap(UserEmailPassword data)
        {
            return new DbUser { Id = Guid.NewGuid(), Email = data.Email, Password = data.Password };
        }

        public UserEmailPassword CreateRemap(DbUser data)
        {
            throw new NotImplementedException();
        }
    }
}
