using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Configurations
{
    public class BlogReactionConfiguration : IEntityTypeConfiguration<BlogReaction>
    {
        public void Configure(EntityTypeBuilder<BlogReaction> builder)
        {
            builder.HasOne(br => br.Blog)
                .WithMany(b => b.BlogReactions)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(br => br.BlogId);
            builder.HasOne(br => br.AppUser)
                .WithMany(b => b.BlogReactions)
                .HasForeignKey(br => br.AppUserId);
            builder.Property(br => br.Reaction)
                .IsRequired();
            builder.Ignore(b => b.IsDeleted);
        }
    }
}
