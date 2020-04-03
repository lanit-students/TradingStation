using System;

namespace DataBaseService.Interfaces
{
    public interface IRepository<Input>
    {
        void Delete(Guid data);
        Guid Create(Input data);
    }
}
