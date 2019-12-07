using Blog.Api.Middleware;
using Blog.Infrastructure.Services;
using Blog.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;

namespace Blog.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder TrySeedData(this IApplicationBuilder app)
        {
            var settings = (GeneralSettings)app.ApplicationServices.GetService(typeof(GeneralSettings));
            if (settings.SeedData)
            {
                var dataInitializer = (IDataInitializer)app.ApplicationServices.GetService(typeof(IDataInitializer));
                dataInitializer.SeedDataAsync();
            }

            return app;
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
            => app.UseMiddleware(typeof(ExceptionHandlerMiddleware));
    }
}
