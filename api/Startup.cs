using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; // var xyz = Configuration["xyz"]
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var connection = Configuration.GetConnectionString("NewsDatabase");
            // DI for controllers
            services.AddDbContext<api.NewsContext>
                (options => options.UseSqlite(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseCors(c => { c.AllowAnyHeader().AllowAnyMethod().WithOrigins(Configuration.GetSection("Appsettings")?["CorsHost"] ?? ""); });
                app.UseDeveloperExceptionPage();
                app.UseMiddleware<Infrastructure.HeavyLoadMiddleware>(3000, true);
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseHttpsRedirection(); // dev: dotnet dev-certs https --trust
            app.UseMvc();

        }
    }
}
