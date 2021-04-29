using BookStore.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using BookStore.Core.Entities;
using BookStore.Infrastructure.Services;
using BookStore.Core.Repository;
using BookStore.Infrastructure.UnitOfWork;
using BookStore.Core.Shared;
using BookStore.Infrastructure.Repositories;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BookStore.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        private readonly IConfiguration Configuration;

        /// <summary>
        /// this method allows us to reach connection string in appsettings file
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.Configure<EnviromentVariables>(Configuration.GetSection("EnviromentVariables"));
            services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BookStoreDB"));
                
            });
            services.AddMvc();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            //services.AddScoped<IGenericRepository, GenericRepository>();
            // Register multi-tenancy
            services.AddMultitenancy<Tenant, TenantResolver>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // if astatic file is requested, serve that without needing to resolve a tenant from the db first.
            app.UseStaticFiles();
            app.UseMultitenancy<Tenant>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "defaultRoute",
                    "{controller=Book}/{action=Index}/{id?}"
                    );
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Book Store API V1");
            });

        }
    }
}
