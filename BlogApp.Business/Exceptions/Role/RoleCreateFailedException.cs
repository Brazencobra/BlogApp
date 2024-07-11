using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Role
{
    public class RoleCreateFailedException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorMessage { get; }

        public RoleCreateFailedException()
        {
            ErrorMessage = "Role yaradılmadı";
        }

        public RoleCreateFailedException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
