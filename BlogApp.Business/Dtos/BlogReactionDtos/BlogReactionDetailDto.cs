using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogReactionDtos
{
    public record BlogReactionDetailDto
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
    }
}
