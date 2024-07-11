using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Role
{
    public class RoleIdException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorMessage { get; }
        public RoleIdException()
        {
            ErrorMessage = "Bu idye uygun role yoxdur";
        }

        public RoleIdException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
