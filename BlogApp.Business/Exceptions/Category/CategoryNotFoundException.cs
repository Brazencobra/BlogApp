using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Exceptions.Category
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() : base("Kateqoriya tapılmadı")
        {
        }

        public CategoryNotFoundException(string? message) : base(message)
        {
        }
    }
}
