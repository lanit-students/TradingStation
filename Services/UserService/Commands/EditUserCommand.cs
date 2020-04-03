using DTO;
using Kernel;
using System;
using UserService.Interfaces;

namespace UserService.Commands
{
    public class EditUserCommand : IEditUserCommand

    {
        public void Execute(UserEmailPassword userEmailPassword)
        {
            try
            {
                CommonValidations.ValidateEmail(userEmailPassword.Email);
                editUser(userEmailPassword);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void editUser(UserEmailPassword userEmailPassword)
        {
            try
            {
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}

