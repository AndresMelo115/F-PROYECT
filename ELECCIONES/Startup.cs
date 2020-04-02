using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ELECCIONES.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using ELECCIONES.LDTO;
using System.Reflection;
using ELECCIONES.Email;




namespace ELECCIONES
  
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
            var emailConfig = Configuration.GetSection("EmailConfiguration").
                Get<EmailConfiguration>();

            services.AddSingleton(emailConfig);

            services.AddScoped<IEmailSender , EmailSenderGmail>();


            services.AddAutoMapper(typeof(AutomapperProfile).GetTypeInfo().Assembly);

            services.AddAutoMapper(typeof(AutoMappinPartidos).GetTypeInfo().Assembly);



            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<EleccionesContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Elecciones")));


            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<EleccionesContext>()

                /*services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 10;
                })*/
                       
                .AddDefaultTokenProviders();


            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    
}
