using AutoMapper;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category,CategoryListItemDto>();
            CreateMap<Category,CategoryDetailDto>();
            CreateMap<CategoryCreateDto,Category>();
            CreateMap<CategoryUpdateDto,Category>();
        }
    }
}
