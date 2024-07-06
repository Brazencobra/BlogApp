using BlogApp.Business.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogDtos
{
    public record BlogCategoryDto
    {
        public CategoryListItemDto Category { get; set; }
    }
}
