using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class UserInfoViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
