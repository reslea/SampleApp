using EsSample.Orders.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using EventStore.ClientAPI;
using System.Net;
using EventStore.ClientAPI.SystemData;
using EsSample.Orders.Database.Entities;
using EsSample.Orders.Extentions;
using EsSample.Orders.OrderSync;

namespace EsSample.Orders
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EsSample.Orders", Version = "v1" });
            });

            services.AddSingleton(new UserCredentials("admin", "changeit"));
            services.AddSingleton<IEventStoreConnection>((_) => {
                var connection = EventStoreConnection.Create(
                   new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1113));

                connection.ConnectAsync().Wait();
                return connection;
            });
            services.AddScoped<IOrderDbSyncronizer, OrderDbSyncronizer>();

            services.AddDbContext<OrdersDbContext>(options => options
                .UseSqlite(@"Data Source=.\Orders.db"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EsSample.Orders v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.EnsureDbCreated<OrdersDbContext>();
            app.EnableOrderStateSyncronisation();
        }
    }
}
