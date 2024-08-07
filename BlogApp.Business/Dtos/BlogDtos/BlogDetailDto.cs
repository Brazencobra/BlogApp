﻿using BlogApp.Business.Dtos.BlogReactionDtos;
using BlogApp.Business.Dtos.CommentDtos;
using BlogApp.Business.Dtos.RoleDtos;
using BlogApp.Business.Dtos.UserDtos;
using BlogApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business.Dtos.BlogDtos
{
    public record BlogDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public int ViewerCount { get; set; }
        public int ReactCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
        public AuthorDto AppUser { get; set; }
        public IEnumerable<BlogCategoryDto> BlogCategories { get; set; }
        public IEnumerable<CommentListItemDto> Comments { get; set; }
        public IEnumerable<BlogReactionListItemDto> BlogReactions { get; set; }
    }
}
