﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public int ViewerCount { get; set; }
        public DateTime CreatedTime { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BlogCategory> BlogCategories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public ICollection<BlogReaction> BlogReactions { get; set; }
    }
}
