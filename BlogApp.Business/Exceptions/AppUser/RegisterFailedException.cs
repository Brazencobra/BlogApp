using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException() : base("Qeydiyyat zamanı problem baş verdi")
        {
        }

        public RegisterFailedException(string? message) : base(message)
        {
        }
    }
}
