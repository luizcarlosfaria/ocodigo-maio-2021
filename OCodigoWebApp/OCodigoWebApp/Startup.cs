using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OCodigoData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OCodigoWebApp
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
            /*
             //Com Cache
            services.AddScoped<IDataAccess, CachedDataAccess>();
            services.AddScoped<DataAccess>();
            */

            /* //Sem Cache
            services.AddScoped<IDataAccess, DataAccess>();
            */


            services.AddScoped<ConnectionManager>();
            services.AddTransient<IDbConnection>(it => new SqlConnection(this.Configuration.GetConnectionString("sgdb")));


            var redisConfiguration = new StackExchange.Redis.Extensions.Core.Configuration.RedisConfiguration();
            Configuration.Bind("Data:Redis", redisConfiguration);

            services.AddStackExchangeRedisExtensions<StackExchange.Redis.Extensions.Newtonsoft.NewtonsoftSerializer>(redisConfiguration);

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
