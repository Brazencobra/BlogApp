using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class UserNotFoundException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status404NotFound;

        public string ErrorMessage { get; }
        public UserNotFoundException() : base("Bele bir user movcud deyil")
        {
            ErrorMessage = "Bele bir user movcud deyil";
        }

        public UserNotFoundException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
