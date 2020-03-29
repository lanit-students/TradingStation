using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;
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
            try
            {
                dbContext.Add(mapper.CreateMap(data));
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message + "Can`t add user");
            }
        }

    }
}

