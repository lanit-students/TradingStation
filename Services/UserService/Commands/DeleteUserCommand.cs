 using System;
 using System.Net;
 using IDeleteUserUserService.Interfaces;

 namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
         public HttpStatusCode Execute(Guid userId)
         {
             return DeleteUserFromDataBaseService(userId) == "Ok" ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
         }

         //TODO fix from stub on request
         private string DeleteUserFromDataBaseService(Guid userId)
         {
             return "Ok";
         }
     }
 }
