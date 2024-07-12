using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.UserDtos
{
    public record AppUserListItemDto 
    {
        public AuthorDto Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
