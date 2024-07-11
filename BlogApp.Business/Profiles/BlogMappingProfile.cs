using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.BlogReactionDtos;
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
            CreateMap<Blog, BlogListItemDto>()
                .ForMember(x => x.ReactCount, blog => blog.MapFrom(x => x.BlogReactions.Count));
            CreateMap<Blog, BlogDetailDto>()
                .ForMember(x => x.ReactCount, blog => blog.MapFrom(x => x.BlogReactions.Count))
                .ReverseMap();
            CreateMap<BlogUpdateDto, Blog>();
            CreateMap<BlogCreateDto , Blog>();
            CreateMap<BlogCategory , BlogCategoryDto>();
            CreateMap<BlogReactionListItemDto , BlogReaction>().ReverseMap();
        }
    }
}
