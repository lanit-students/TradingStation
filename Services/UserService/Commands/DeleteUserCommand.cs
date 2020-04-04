 using System;

 using IDeleteUserUserService.Interfaces;

 namespace UserService.Commands
 {
     public class DeleteUserCommand : IDeleteUserCommand
     {
         public bool Execute(Guid userId)
         {
            return DeleteUser(userId);
         }

         //TODO fix from stub on request
         private bool DeleteUser(Guid userId)
         {
             return false;
         }
     }
 }
