using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.RoleDtos
{
    public record RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
