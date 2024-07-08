using BlogApp.Business.Exceptions.Category;
using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace BlogApp.API.Helpers
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(handlerApp =>
            {
                handlerApp.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    if (feature?.Error is IBaseException ex)
                    {
                        context.Response.StatusCode = ex.StatusCode;
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = ex.StatusCode,
                            Message = ex.ErrorMessage
                        });
                    }
                    else if (feature?.Error is CategoryNotFoundException)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = StatusCodes.Status404NotFound,
                            Message = feature?.Error.Message
                        });
                    }
                    if (feature?.Path == "/")
                    {
                        await context.Response.WriteAsync(" Page: Home.");
                    }
                });
            });
        }
    }
}
