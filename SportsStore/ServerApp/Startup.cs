using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp
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
            string connectionString;
            if( Environment.MachineName == "DESKTOP-ALANM"){
                connectionString = Configuration["ConnectionStrings:Home"];
            }
            else if(Environment.MachineName == "LAPTOP-ALANM"){
                connectionString = Configuration["ConnectionStrings:Laptop"];
            }
            else if(Environment.MachineName == "DESKTOP-FJS6ERP"){
                Console.WriteLine("Job Machine!");
                connectionString = Configuration["ConnectionStrings:DefaultConnection"];  
            }
            else{
                connectionString = "";
            }
            services.AddDbContext<DataContext> (options => options.UseSqlServer(connectionString));
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSpa(spa => {
                string strategy = Configuration.GetValue<string>("DevTools:ConnectionStrategy");
                if(strategy == "proxy"){
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else if(strategy == "managed"){
                    Console.WriteLine("managed");
                    spa.Options.SourcePath = "../ClientApp";
                    spa.UseAngularCliServer("start");
                }
                
            });
            SeedData.SeedDatabase(services.GetRequiredService<DataContext>());
        }
    }
}