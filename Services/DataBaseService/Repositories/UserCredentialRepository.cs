using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;
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

        public void Create(UserEmailPassword data)
        {
            //TODO change to custom exception
            try
            {
                dbContext.Add(mapper.CreateMap(data));
                dbContext.SaveChanges();
            }
            catch(SqlException e)
            {
                Console.WriteLine(e.Message + "\nCan`t add user :(");
                throw new Exception(e.Message + "\nCan`t add user");       
            }
        }

        public void Delete(Guid data)
        {
            var user = dbContext.Find<DbUserCredential>(data);
            if (!(user is null))
            {
                user.IsExist = false;
                dbContext.SaveChanges();
            }
        }
    }
}

