using System;

namespace DataBaseService.Interfaces
{
    public interface IRepository<Input>
    {
        Guid Create(Input data);
    }
}
