using DataBaseService.Database;
using DataBaseService.Repositories.Interfaces;
using DataBaseService.Mappers.Interfaces;
using DTO;
using System.Linq;
using System;
using Kernel.CustomExceptions;
using DataBaseService.Database.Models;

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
            dbContext.UsersCredentials.Add(mapper.MapToDbUserCredential(userCredential));
            dbContext.SaveChanges();
        }

        public UserCredential GetUserCredential(string email)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email);

            return mapper.MapUserCredential(dbCredential);
        }

        public User GetUserById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(uc => uc.Id == userId);
            if (dbUser == null)
            {
                throw new NotFoundException("User not found");
            }

            var email = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId).Email;
            return mapper.MapUser(dbUser, email);
        }

        public void DeleteUser(Guid userId)
        {
            var dbUserCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.UserId == userId);

            if (dbUserCredential is null)
            {
                throw new NotFoundException("Not found User for delete");
            }

            if(!dbUserCredential.IsActive)
            {
                throw new BadRequestException("User was deleted early or not confirmed");
            }

            dbUserCredential.IsActive = false;
            dbContext.SaveChanges();
        }
    }
}
