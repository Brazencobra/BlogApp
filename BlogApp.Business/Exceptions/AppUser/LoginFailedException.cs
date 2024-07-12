using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class LoginFailedException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status401Unauthorized;

        public string ErrorMessage { get; }
        public LoginFailedException() : base("Giriş uğursuz oldu")
        {
            ErrorMessage = "Giriş uğursuz oldu";
        }

        public LoginFailedException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
