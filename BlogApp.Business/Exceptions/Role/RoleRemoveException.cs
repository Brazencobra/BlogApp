using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Role
{
    public class RoleRemoveException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorMessage { get; }
        public RoleRemoveException()
        {
            ErrorMessage = "Role silinerken problem bas verdi";
        }

        public RoleRemoveException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
