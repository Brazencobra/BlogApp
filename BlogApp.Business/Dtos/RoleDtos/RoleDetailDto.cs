using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.RoleDtos
{
    public record RoleDetailDto
    {
        public RoleDto Roles { get; set; }
        public IEnumerable<AuthorDto> Users { get; set; }
    }
}
