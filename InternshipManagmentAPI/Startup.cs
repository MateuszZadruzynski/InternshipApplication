using Administration.Interface;
using Administration.Controllers;
using DataAcess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternshipManagmentAPI.Utilities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using InternshipApplicationServer.ServiceInterface;
using InternshipApplicationServer.Service;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace InternshipManagmentAPI
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
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>().
              AddEntityFrameworkStores<AppDbContext>().
              AddDefaultTokenProviders();

            var security = Configuration.GetSection("Security");
            services.Configure<Security>(security);

            var apiSecurity = security.Get<Security>();
            var key = Encoding.ASCII.GetBytes(apiSecurity.Key);

            services.AddAuthentication(
                o => {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(j =>
            {
                j.RequireHttpsMetadata = false;
                j.SaveToken = true;
                j.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = apiSecurity.Issuer,
                    ValidAudience = apiSecurity.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<CompanyInterface, CompanyController>();
            services.AddScoped<DiaryInterface, DiaryController>();
            services.AddScoped<InternshipInterface, InternshipController>();
            services.AddScoped<KeeperInterface, KeeperController>();
            services.AddScoped<StudentInterface, StudentController>();
            services.AddScoped<CompanyImageInterface, CompanyImageController>();
            services.AddScoped<KeeperAvatarsInterface, KeeperAvatarsController>();
            services.AddScoped<StudentAvatarsInterface, StudentAvatarsController>();
            services.AddScoped<StudentKeeperInterface, StudentKeeperController>();
            services.AddScoped<KeeperAvatarsInterface, KeeperAvatarsController>();
            services.AddScoped<QuestionnaireInterface, QuestionnaireController>();
            services.AddScoped<EmailInterface,EmailSender>();
            services.AddCors(c => c.AddPolicy("EasyInternships", builder =>
            {
                builder.AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowAnyOrigin();
             }));

            services.AddRouting(r => r.LowercaseUrls = true);

            services.AddControllers().AddJsonOptions(O => O.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(n =>
            {
                n.SerializerSettings.ContractResolver = new DefaultContractResolver();
                n.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InternshipManagmentAPI", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Place Bearer and then token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new List<string>()
                    }
                }) ;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InternshipManagmentAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseCors("EasyInternships");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
