using System;

namespace DataBaseService.Interfaces
{
    public interface IRepository<Input>
    {
        void Create(Input data);
        void Delete(Guid data);
    }
}
