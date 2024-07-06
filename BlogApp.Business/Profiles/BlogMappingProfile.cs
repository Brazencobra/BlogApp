using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CategoryDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Profiles
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile()
        {
            CreateMap<Blog , BlogListItemDto>();
            CreateMap<Blog , BlogDetailDto>();
            CreateMap<BlogUpdateDto, Blog>();
            CreateMap<BlogCreateDto , Blog>();
            CreateMap<BlogCategory , BlogCategoryDto>();
        }
    }
}
