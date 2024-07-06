using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() : base("There was a problem during registration")
        {
        }

        public RegisterFailedException(string? message) : base(message)
        {
        }
    }
}
