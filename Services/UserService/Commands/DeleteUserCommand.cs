 using System;
 using System.Net;
 using IDeleteUserUserService.Interfaces;


 namespace UserService.Commands

 {
     public class DeleteUserCommand : IDeleteUser<Guid, string>
     {

         public HttpStatusCode Execute(Guid userId)
         {
             if (DeleteUserFromDataBaseService(userId) == "Ok")
                 return HttpStatusCode.OK;
             else
             {
                 return HttpStatusCode.BadRequest;
             }
         }

        //TODO fix from stub on request
         private string DeleteUserFromDataBaseService(Guid userId)
         {
             return "Ok";
         }
     }
 }