using BlogApp.Business.HelperServices.Implements;
using BlogApp.Business.HelperServices.Interfaces;
using BlogApp.Business.Services.Implements;
using BlogApp.Business.Services.Interfaces;
using BlogApp.DAL.Repositories.Implements;
using BlogApp.DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Business
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection service)
        {
            service.AddScoped<ICategoryService, CategoryService>();
            service.AddScoped<IBlogService, BlogService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<ITokenHandler, TokenHandler>();
            service.AddScoped<ICommentService , CommentService>();
            service.AddHttpContextAccessor();
        }
    }
}
