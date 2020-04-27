using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
    public interface ISecretTokenEngine
    {
        public Guid GetToken(string email);
        public string GetEmail(Guid token);
    }
}
 