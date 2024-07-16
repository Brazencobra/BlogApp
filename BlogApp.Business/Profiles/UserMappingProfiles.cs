using AutoMapper;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Profiles
{
    public class UserMappingProfiles : Profile
    {
        public UserMappingProfiles()
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<AppUser, AuthorDto>().ReverseMap();
            CreateMap<AppUserListItemDto, AppUser>().ReverseMap();
            CreateMap<AppUser , AppUserDetailDto>().ReverseMap(); 

        }
    }
}
