using AutoMapper;
using BlogApp.Business.Services.Implements;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Profiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<IdentityRole,RoleDetailDto>().ReverseMap();
        }
    }
}
