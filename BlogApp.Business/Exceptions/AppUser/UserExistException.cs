using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class UserExistException : Exception
    {
        public UserExistException() : base("Loqin və ya E-mail mövcuddur")
        {
        }

        public UserExistException(string? message) : base(message)
        {
        }
    }
}
