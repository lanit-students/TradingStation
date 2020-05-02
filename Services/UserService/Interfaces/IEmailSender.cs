using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Interfaces
{
   public interface IEmailSender
    {
        void SendEmail(string email, ISecretTokenEngine secretTokenEngine);
    }
}
