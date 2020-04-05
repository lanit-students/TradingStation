using System;
using System.Linq;

using DataBaseService.Database;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;

using DTO;
using Kernel.CustomExceptions;

namespace DataBaseService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserMapper mapper;
        private readonly TPlatformDbContext dbContext;

        public UserRepository(IUserMapper mapper, TPlatformDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public void CreateUser(User user)
        {
            dbContext.Users.Add(mapper.MapToDbUser(user));
            dbContext.SaveChanges();
        }

        public void CreateUserCredential(UserCredential userCredential)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == userCredential.Email);

            if (dbCredential is null)
            {
                dbContext.UsersCredentials.Add(mapper.MapToDbUserCredential(userCredential));
                dbContext.SaveChanges();
            }
            else
            {
                throw new ForbiddenException("User not found");
            }
            
        }

        public UserCredential GetUserCredential(string email)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email);
            if (dbCredential is null)
            {
                throw new ForbiddenException("User not found");               
            }

            return mapper.MapUserCredential(dbCredential);
        }

        public UserCredential GetUserCredentialById(Guid Id)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Id == Id);

            if (dbCredential is null)
            {
                throw new ForbiddenException("User not found");
            }

            return mapper.MapUserCredential(dbCredential);
        }
    }
}

