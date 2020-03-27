using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseService.Interfaces
{
    interface IMapper<B, Db>
    {
        Db CreateMap(Db data);
        B CreateRemap(B data);
    }
}
