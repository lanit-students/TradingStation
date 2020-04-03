using System;
using DTO;

namespace GUI.Scripts
{
    public static class UserEditor
    {
        /// <summary>
        /// Edits information about the user
        /// </summary>
        /// <param name="id">Identificator no know who to change</param>
        /// <param name="userInfo">Updated information</param>
        /// <param name="oldPassword">If not null, there is an attempt to change password</param>
        /// <param name="newPassword">Same as with oldPassword</param>
        public static void EditUser(Guid id, UserInformation userInfo, string oldPassword = null, string newPassword = null)
        {
            // I don't want to try creating any implementation here because
            // this command has not been implemented in UserService yet
            // so there's no documentation for usage or anything like that.
        }
    }
}
