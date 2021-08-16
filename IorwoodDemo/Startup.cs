using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IorwoodDemo.DataAccess.Data;
using IorwoodDemo.DataAccess.UnitOfWork.Abstract;
using IorwoodDemo.DataAccess.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using IorwoodDemo.Utility;
using IorwoodDemo.Areas.JWTAuth.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using IorwoodDemo.Areas.JWTAuth.Helpers;
using IorwoodDemo.Areas.JWTAuth.Helpers.Abstract;
using IorwoodDemo.Model.Entity;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.OpenApi.Models;
using IorwoodDemo.DataAccess.Initializer;

namespace IorwoodDemo
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
            services.AddDbContext<IorwoodDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();

            

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JwtTokenConfig>();
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });


            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IorwoodDbContext>();
            //            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
            //.AddEntityFrameworkStores<IorwoodDbContext>();

            //services.AddIdentity<IdentityUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = true; options.c })
            //.AddEntityFrameworkStores<IorwoodDbContext>().AddDefaultTokenProviders();



            //var builder = services.AddIdentityCore<ApplicationUser>(o =>
            //{
            //    // configure identity options
            //    o.Password.RequireDigit = false;
            //    o.Password.RequireLowercase = false;
            //    o.Password.RequireUppercase = false;
            //    o.Password.RequireNonAlphanumeric = false;
            //    o.Password.RequiredLength = 6;
            //}).AddRoles<IdentityRole>();
            //builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);

            //builder.AddEntityFrameworkStores<IorwoodDbContext>().AddDefaultTokenProviders();




            //  services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IorwoodDbContext>();
            //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            //.AddEntityFrameworkStores<IorwoodDbContext>().AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IDbInitializer, DBInitializer>();

            services.AddCors(x =>
            {
                x.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin().
                    AllowAnyHeader().
                    AllowAnyMethod();
                });
            });
            //            
             

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            //services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbInitializer dbInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseSession();
            app.UseHttpsRedirection();
          
            app.UseStaticFiles();

            app.UseCors("EnableCORS");
            app.UseRouting();
            dbInit.Initialize();
            app.UseAuthentication();
           
            app.UseAuthorization();
app.UseCors(builder => builder
              .AllowAnyOrigin()
              //.WithOrigins("http://localhost:8080")
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
               
                endpoints.MapRazorPages();
            });
        }
    }
}
