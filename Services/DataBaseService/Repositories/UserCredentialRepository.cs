using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;
using Kernel.CustomExceptions;
using Microsoft.Data.SqlClient;
using System;

namespace DataBaseService.Repositories
{
    public class UserCredentialRepository : IRepository<UserEmailPassword>
    {
        private readonly IMapper<UserEmailPassword, DbUserCredential> mapper;
        private readonly DataBaseContext dbContext;

        public UserCredentialRepository(IMapper<UserEmailPassword, DbUserCredential> mapper, DataBaseContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public Guid Create(UserEmailPassword data)
        {
            //TODO change to custom exception
            try
            {
                var user = mapper.CreateMap(data);
                dbContext.Add(user);
                dbContext.SaveChanges();
                return user.Id;
            }
            catch(SqlException e)
            {
                throw new Exception(e.Message + "\nCan`t add user");
            }
        }

        public void Delete(Guid id)
        {
            var user = dbContext.Find<DbUserCredential>(id);
            if (!(user is null))
            {
                user.IsExists = false;
                dbContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException("User with such id is not found");
            }
        }
    }
}
