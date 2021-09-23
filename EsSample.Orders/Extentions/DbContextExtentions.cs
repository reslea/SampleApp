using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EsSample.Orders.Extentions
{
    public static class DbContextExtentions
    {
        public static void EnsureDbCreated<TDbContext>(this IApplicationBuilder app)
            where TDbContext : DbContext
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<TDbContext>();

            context.Database.EnsureCreated();
        }
    }
}
