using BlogApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(b => b.Description)
                .IsRequired();
            builder.Property(b=>b.CoverImage)
                .IsRequired();
            builder.Property(b => b.CreatedTime)
                .HasDefaultValueSql("getutcdate()");
            builder.HasOne(u => u.AppUser)
                .WithMany(b => b.Blogs)
                .HasForeignKey(b => b.AppUserId); 
            builder.Property(b=>b.ViewerCount)
                .HasDefaultValue (0);
        }
    }
}
