using BlogApp.Core.Entities.Commons;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Common
{
    public class NotFoundException<T> : Exception , IBaseException where T : BaseEntity , new()
    {
        public int StatusCode => StatusCodes.Status404NotFound;
        public string ErrorMessage { get; }
        public NotFoundException() : base()
        {
            ErrorMessage = $"{typeof(T).Name} tapılmadı";
        }

        public NotFoundException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

        
    }
}
