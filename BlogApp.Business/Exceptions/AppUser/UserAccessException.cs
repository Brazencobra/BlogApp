using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class UserAccessException : Exception , IBaseException 
    {
        public int StatusCode => StatusCodes.Status403Forbidden;

        public string ErrorMessage { get; }
        public UserAccessException() : base()
        {
            ErrorMessage = "Istifadecinin icazesi yoxdur";
        }

        public UserAccessException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
