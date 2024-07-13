using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.AppUser
{
    public class RefreshTokenExpiredException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status419AuthenticationTimeout;

        public string ErrorMessage { get; }
        public RefreshTokenExpiredException()
        {
            ErrorMessage = "Yeniden login olmalisiniz";
        }

        public RefreshTokenExpiredException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
