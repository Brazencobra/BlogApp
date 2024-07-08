using BlogApp.Business.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public class CommentChildDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public AuthorDto AppUser { get; set; }
    }
}
