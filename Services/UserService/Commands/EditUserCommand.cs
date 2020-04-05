using DTO.RestRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        public EditUserCommand()
        {

        }
        

        public Task<bool> Execute(EditUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
