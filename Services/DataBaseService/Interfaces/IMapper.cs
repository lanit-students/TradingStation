using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    public interface IMapper<B, Db>
    {
        Db CreateMap(B data);
        B CreateRemap(Db data);
    }
}
