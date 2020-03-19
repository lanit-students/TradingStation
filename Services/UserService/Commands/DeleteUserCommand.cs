 using System;
 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using IDeleteUserUserService.Interfaces;
 using Microsoft.AspNetCore.Mvc;

 namespace UserService.Commands
 
 {
     public class DeleteUserCommand : IDeleteUser<Guid, string>
     {
         
         public string Execute(Guid userId)
         {
             DeleteUserFromDataBaseService(userId);
             return "Bravo";
         }
         private void DeleteUserFromDataBaseService(Guid userId)
         {
             return ;
         }
    }
 }
