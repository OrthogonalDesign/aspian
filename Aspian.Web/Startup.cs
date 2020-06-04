using System;
using Aspian.Application.Core.TaxonomyServices;
using Aspian.Domain.UserModel;
using Aspian.Persistence;
using Aspian.Web.Middleware.API;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Aspian.Web
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
            services.AddDbContext<DataContext>(options =>
            {
                // Providing Lazy Loading service for Entity Framework models
                options.UseLazyLoadingProxies();
                // Using MSSQL Server driver and providing access to connection string
                options.UseSqlServer(Configuration.GetConnectionString("AspianConnection"));
            });

            // Providing full mvc ASP.NET Core framework
            services.AddControllersWithViews()
                    // .ConfigureApiBehaviorOptions(options =>
                    // {
                    //     options.SuppressConsumesConstraintForFormFileParameters = true;
                    //     options.SuppressInferBindingSourcesForParameters = true;
                    //     options.SuppressModelStateInvalidFilter = true;
                    //     options.SuppressMapClientErrors = true;
                    //     options.ClientErrorMapping[StatusCodes.Status404NotFound].Link =
                    //         "https://httpstatuses.com/404";
                    // })
                    // providing FluentValidation service for Aspian.Application.Core Assembly
                    .AddFluentValidation(cfg =>
                    {
                        cfg.RegisterValidatorsFromAssemblyContaining<Create>();
                    });

            // Identity services
            var builder = services.AddIdentityCore<User>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<DataContext>();
            identityBuilder.AddSignInManager<SignInManager<User>>();
            services.AddAuthentication();

            // Providing MediatR service for Aspian.Application.Core Assembly
            services.AddMediatR(typeof(List.Handler).Assembly);
            // Providing AutoMapper service for Aspian.Application.Core Assembly
            services.AddAutoMapper(typeof(List.Handler).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWhen(ctx => ctx.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase),
                    appBuilder =>
                    {
                        // Custom API Error handler
                        appBuilder.UseMiddleware<APIErrorHandlingMiddleware>();
                    }
                );

                app.UseWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase),
                    appBuilder =>
                    {
                        // Default mvc error handler
                        appBuilder.UseDeveloperExceptionPage();
                    }
                );
            }
            else
            {
                app.UseWhen(ctx => ctx.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase),
                    appBuilder =>
                    {
                        // Custom API Error handler
                        appBuilder.UseMiddleware<APIErrorHandlingMiddleware>();
                    }
                );

                app.UseWhen(ctx => !ctx.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase),
                    appBuilder =>
                    {
                        // Default mvc error handler
                        appBuilder.UseExceptionHandler("/Home/Error");
                    }
                );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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