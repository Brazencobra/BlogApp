using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class UserAccessException : Exception
    {
        public UserAccessException() : base("Istifadecinin icazesi yoxdur")
        {
        }

        public UserAccessException(string? message) : base(message)
        {
        }
    }
}
