using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    public interface IRepository<T>
    {
        void Create(T data);
    }
}
