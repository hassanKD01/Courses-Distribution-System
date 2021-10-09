using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Courses_Distribution_System.Data;
using Courses_Distribution_System.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Courses_Distribution_System
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddRazorPages()
                .AddSessionStateTempDataProvider();

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options=>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath="/AccessDenied";
                    options.Cookie.HttpOnly = true;
                })
                .AddCookie("Admin",options=>
                {
                    options.LoginPath = "/Login";
                    options.AccessDeniedPath = "/AccessDenied";
                    options.Cookie.HttpOnly = true;
                });
            services.AddAuthorization(options => {
                options.AddPolicy(
                    "Admin",
                    policyBuilder => { 
                        policyBuilder.RequireRole("Admin");
                        policyBuilder.AddAuthenticationSchemes("Admin");
                    });
                options.AddPolicy(
                    "HeadOfDepartment",
                    policyBuilder=>
                    {
                        policyBuilder.RequireRole("HeadOfDepartment");
                        policyBuilder.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                    });
            });
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ExcelService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        //Registring Autofac container
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
