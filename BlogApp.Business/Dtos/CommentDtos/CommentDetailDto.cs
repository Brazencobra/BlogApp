using BlogApp.Business.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.CommentDtos
{
    public record CommentDetailDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public AuthorDto AppUser { get; set; }
        public IEnumerable<CommentChildDto> Children { get; set; }
    }
}
