 using System;

 using IDeleteUserUserService.Interfaces;

 namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
         public void Execute(Guid userId)
         {
            DeleteUser(userId);
         }

         //TODO fix from stub on request
         private string DeleteUser(Guid userId)
         {
             return "Ok";
         }
     }
 }
