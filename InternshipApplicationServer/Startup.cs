using DataAcess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Administration.Controllers;
using Administration.Interface;
using InternshipApplicationServer.ServiceInterface;
using InternshipApplicationServer.Service;
using Microsoft.AspNetCore.Identity;

namespace InternshipApplicationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>().
              AddEntityFrameworkStores<AppDbContext>().
              AddDefaultTokenProviders().AddDefaultUI();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<CompanyInterface, CompanyController>();
            services.AddScoped<DiaryInterface, DiaryController>();
            services.AddScoped<InternshipInterface, InternshipController>();
            services.AddScoped<KeeperInterface, KeeperController>();
            services.AddScoped<StudentInterface, StudentController>();
            services.AddScoped<CompanyImageInterface, CompanyImageController>();
            services.AddScoped<KeeperAvatarsInterface, KeeperAvatarsController>();
            services.AddScoped<StudentAvatarsInterface, StudentAvatarsController>();
            services.AddScoped<IdentityRoleUserInterface, IdentityRoleUser>();
            services.AddScoped<StudentKeeperInterface, StudentKeeperController>();
            services.AddScoped<QuestionnaireInterface, QuestionnaireController>();
            services.AddScoped<EmailInterface, EmailSender>();
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityRoleUserInterface initializer)
        {
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
            initializer.AuthenticationRole();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
