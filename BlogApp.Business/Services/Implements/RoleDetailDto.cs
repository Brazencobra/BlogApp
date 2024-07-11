using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Services.Implements
{
    public class RoleDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AppUser AppUser { get; set; }
    }
}
