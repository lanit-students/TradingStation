using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    interface ICommand<in I, out T>
    {
        T Execute(I data);
    }
}
