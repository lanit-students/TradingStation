using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    interface IRepository<T>
    {
        void Create(T data);
    }
}
