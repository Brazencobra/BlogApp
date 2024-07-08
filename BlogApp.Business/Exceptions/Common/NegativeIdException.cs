using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Common
{
    public class NegativeIdException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;

        public string ErrorMessage { get; }
        public NegativeIdException() : base()
        {
            ErrorMessage = "Id 0-dan kiçik və ya bərabər ola bilməz";
        }

        public NegativeIdException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
