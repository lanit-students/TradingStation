 using System;
 using System.Collections.Generic;
using System.Linq;
 using System.Net;
 using System.Threading.Tasks;
 using IDeleteUserUserService.Interfaces;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.CodeAnalysis.CSharp.Syntax;

 namespace UserService.Commands
 
 {
     public class DeleteUserCommand : IDeleteUser<Guid, string>
     {

         public string Execute(Guid userId)
         {
            if(DeleteUserFromDataBaseService(userId) == "Ok")
            return "HttpStatusCode.OK";
            else
            {
                return "400";
            }
        }
         private string DeleteUserFromDataBaseService(Guid userId)
         {
             return "Ok";
         }
    }
 }
