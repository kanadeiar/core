using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication1.Dal.Context;
using WebApplication1.Data;
using WebApplication1.Infrastructure.Conventions;
using WebApplication1.Infrastructure.Middleware;
using WebApplication1.Services;
using WebApplication1.Services.Interfaces;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Application1DB>(opt => 
                opt.UseSqlServer(Configuration.GetConnectionString("Default"), o => o.EnableRetryOnFailure()));
            services.AddTransient<WebStoreDBInitializer>();

            services.AddSingleton<IPersonsData, InMemoryEmployesData>(); // �������� �� ����� ������ ����������
            //services.AddScoped<IPersonsData, InMemoryEmployesData>(); // �������� ������ �� ����� �������
            //services.AddTransient<IPersonsData, InMemoryEmployesData>(); // ��� ������ ��� ��������
            services.AddSingleton<IProductData, InMemoryProductData>();

            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConvention()))
                .AddRazorRuntimeCompilation();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            using (var scope = service.CreateScope() )
                scope.ServiceProvider.GetRequiredService<WebStoreDBInitializer>().Init();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware(typeof(TestMiddleware));

            //��������� ����������� �������
            app.Map("/Hello", context => context.Run(async req => await req.Response.WriteAsync("Hello!")));

            app.Use(async (app, next) =>
            {
                var proc = next();
                await proc;
            });

            app.UseWelcomePage("/WelcomePage");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/hello", async context =>
                {
                    await context.Response.WriteAsync($"Hello World, {Configuration["Hello"]}!");
                });

                //endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
