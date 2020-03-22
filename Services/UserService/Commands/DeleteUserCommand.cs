 using System;
 using System.Net;
 using IDeleteUserUserService.Interfaces;

 namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
         public void Execute(Guid userId)
         {
            DeleteUserFromDataBaseService(userId);
         }

         //TODO fix from stub on request
         private string DeleteUserFromDataBaseService(Guid userId)
         {
             return "Ok";
         }
     }
 }
