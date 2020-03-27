using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    public interface ICommand<I>
    {
        void Execute(I Data);
    }
    public interface ICommand<in I, out T>
    {
        T Execute(I data);
    }    
}
