using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Bele bir user movcud deyil")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
