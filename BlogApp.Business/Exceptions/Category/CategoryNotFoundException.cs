using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Category
{
    public class CategoryNotFoundException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status404NotFound;

        public string ErrorMessage { get; }
        public CategoryNotFoundException() : base()
        {
            ErrorMessage = "Kateqoriya tapılmadı";
        }

        public CategoryNotFoundException(string? message) : base(message)
        {
            ErrorMessage = message;
        }

    }
}
