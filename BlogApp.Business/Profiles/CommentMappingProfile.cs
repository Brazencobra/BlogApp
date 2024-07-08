using AutoMapper;
using BlogApp.Business.Dtos.BlogDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Profiles
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentListItemDto>();
            CreateMap<Comment, CommentDetailDto>();
            CreateMap<CommentChildDto , Comment>().ReverseMap();
            CreateMap<CommentCreateDto , Comment>();
            CreateMap<CommentUpdateDto , Comment>();
        }
    }
}
