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
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using System.IO;

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
            Console.WriteLine($"Machine name = {Environment.MachineName}!");
            string connectionString;
            if( Environment.MachineName == "DESKTOP-ALANM"){
                connectionString = Configuration["ConnectionStrings:Home"];
            }
            else if(Environment.MachineName == "LAPTOP-ALANM"){
                Console.WriteLine("confirmed laptop");
                connectionString = Configuration["ConnectionStrings:HomeLaptop"];
            }
            else if(Environment.MachineName == "DESKTOP-FJS6ERP"){
                Console.WriteLine("Job Machine!");
                connectionString = Configuration["ConnectionStrings:DefaultConnection"];  
            }
            else{
                connectionString = "";
            }
            services.AddDbContext<DataContext> (options => options.UseSqlServer(connectionString));
            services.AddControllersWithViews().AddJsonOptions( opts => { 
                opts.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddNewtonsoftJson();// AddNewtonsoftJson allows me to parse JSON "patch" requests   | dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson |    optional => [--version 3.0.0]
            services.AddRazorPages();
            services.AddSwaggerGen( 
                options => 
                { 
                    options.SwaggerDoc("v1", 
                        new OpenApiInfo{
                        Title = "SportsStore API", Version = "v1"
                        });
            });
            services.AddDistributedSqlServerCache(
                options => {
                    options.ConnectionString = connectionString;
                    options.SchemaName = "dbo";
                    options.TableName = "SessionData";
                }
            );
            services.AddSession(
                options => {
                    options.Cookie.Name = "SportsStore.Session";
                    options.IdleTimeout = System.TimeSpan.FromHours(48);
                    options.Cookie.HttpOnly = false;//must be set false so angular app will access it
                    options.Cookie.IsEssential = true;
                }
            );
            services.AddResponseCompression(
                opts => {
                    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream"});
                }
            );
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

            app.UseStaticFiles(
                new StaticFileOptions{
                    RequestPath = "/blazor",
                    FileProvider = new PhysicalFileProvider( Path.Combine(Directory.GetCurrentDirectory(), "../BlazorApp/wwwroot"))
                }
            );

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                    
                endpoints.MapControllerRoute(
                    name: "angular_fallback",
                    pattern: "{target:regex(store|cart|checkout)}/{*catchall}",
                    defaults:new {controller = "Home", action = "Index"}
                );
                endpoints.MapControllerRoute(
                    name: "blazor_integration",
                    pattern: "/blazor/{*path:nonfile}",
                    defaults: new {controller = "Home", action = "Blazor"}
                );
                // endpoints.MapFallbackToClientSideBlazor<BlazorApp.Startup>("blazor/{*path:nonfile}", "index.html");    
            });
            app.Map("/blazor", opts => opts.UseClientSideBlazorFiles<BlazorApp.Startup>());
            app.UseClientSideBlazorFiles<BlazorApp.Startup>();
            app.UseSwagger();
            app.UseSwaggerUI(
                options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SportsStore API");
                }
            );
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
