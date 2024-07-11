using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogReactionDtos
{
    public record BlogReactionListItemDto
    {
        public AuthorDto AppUser { get; set; }
        public Reactions Reaction { get; set; }
    }
}
