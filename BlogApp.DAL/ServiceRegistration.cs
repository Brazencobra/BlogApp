using BlogApp.DAL.Repositories.Implements;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DAL
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<ICategoryRepository, CategoryRepository>();

            service.AddScoped<IBlogRepository, BlogRepository>();

            service.AddScoped<ICommentRepository , CommentRepository>();

            service.AddScoped<IBlogReactionRepository , BlogReactionRepository>();
        }
    }
}
