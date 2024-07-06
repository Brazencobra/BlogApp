using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException() : base("Giriş uğursuz oldu")
        {
        }

        public LoginFailedException(string? message) : base(message)
        {
        }
    }
}
