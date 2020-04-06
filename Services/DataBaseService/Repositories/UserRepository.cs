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
            var dbCredential = dbContext.UsersCredentials
                .FirstOrDefault(uc => uc.Email == userCredential.Email);

            if (dbCredential == null)
            {
                throw new BadRequestException("User already exist");
            }
            else
            {
                dbContext.UsersCredentials.Add(mapper.MapToDbUserCredential(userCredential));
                dbContext.SaveChanges();
            }
        }

        public UserCredential GetUserCredential(string email)
        {
            var dbCredential = dbContext.UsersCredentials.FirstOrDefault(uc => uc.Email == email);
            if (dbCredential == null)
            {
                throw new NotFoundException("User not found");
            }

            return mapper.MapUserCredential(dbCredential);
        }

        public User GetUserById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(uc => uc.Id == userId);
            if (dbUser == null)
            {
                throw new NotFoundException("User not found");
            }
            return mapper.MapUser(dbUser);
        }

        public void DeleteUser(Guid userId)
        {
            var dbUserCredential = dbContext.Find<DbUserCredential>(userId);
            if (dbUserCredential == null)
            {
                throw new ForbiddenException("Not found User for delete");
            }
            if(dbUserCredential.IsActive==false)
            {
                throw new ForbiddenException("User was deleted early");
            }
             dbUserCredential.IsActive = false;
             dbContext.SaveChanges();
        }
    }
}

