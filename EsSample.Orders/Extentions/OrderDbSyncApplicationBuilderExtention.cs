using EsSample.Orders.Database;
using EsSample.Orders.Database.Entities;
using EsSample.Orders.OrderSync;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EsSample.Orders.Extentions
{
    public static class OrderDbSyncApplicationBuilderExtention
    {
        public static void EnableOrderStateSyncronisation(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            var syncronizer = scope.ServiceProvider.GetService<IOrderDbSyncronizer>();

            syncronizer.ProcessExistingEvents();
            syncronizer.SubscribeToFutureEvents();
        }
    }

}
