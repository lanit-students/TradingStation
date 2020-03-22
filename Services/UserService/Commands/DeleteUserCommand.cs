 using System;
 using System.Net;
 using IDeleteUserUserService.Interfaces;


 namespace UserService.Commands

 {
     public class DeleteUserCommand : IDeleteUser
     {
         public HttpStatusCode Execute(int userId)
         {
             return DeleteUserFromDataBaseService(userId) == "Ok" ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
         }

        //TODO fix from stub on request
         private string DeleteUserFromDataBaseService(int userId)
         {
             return "Ok";
         }
     }
 }