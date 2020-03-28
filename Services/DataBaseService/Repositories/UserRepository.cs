using DataBaseService.DbModels;
using DataBaseService.Interfaces;
using DTO;

namespace DataBaseService.Repositories
{
    public class UserRepository : IRepository<UserEmailPassword>
    {
        readonly IMapper<UserEmailPassword, DbUser> mapper;
        readonly DataBaseContext dbContext;        
        
        public UserRepository(IMapper<UserEmailPassword, DbUser> mapper, DataBaseContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }
        public void Create(UserEmailPassword data)
        {
            dbContext.Add(mapper.CreateMap(data));
            dbContext.SaveChanges();
        }

    }
}

