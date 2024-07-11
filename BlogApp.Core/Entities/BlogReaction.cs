using BlogApp.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Entities
{
    public class BlogReaction : BaseEntity
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
        public Reactions Reaction { get; set; }
    }
}
