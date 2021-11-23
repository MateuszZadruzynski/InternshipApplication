using Blazored.LocalStorage;
using InternshipApplicationClient.Interface;
using InternshipApplicationClient.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace InternshipApplicationClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIURL")) });
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationToken>();
            builder.Services.AddScoped<AuthenticationInterface, AuthenticationService>();
            builder.Services.AddScoped<CompanyInterface, CompanyService>();
            builder.Services.AddScoped<InternshipInterface, InternshipService>();
            builder.Services.AddScoped<StudentInterface, StudentService>();
            builder.Services.AddScoped<KeeperInterface, KeeperService>();
            builder.Services.AddScoped<DiaryInterface, DiaryService>();
            builder.Services.AddScoped<StudentKeeperInterface, StudentKeeperService>();
            builder.Services.AddScoped<EmailInterface, EmailService>();
            builder.Services.AddScoped<UploadInterface, UploadService>();
            builder.Services.AddScoped<StudentAvatarInterface, StudentAvatarService>();
            builder.Services.AddScoped<KeeperAvatarInterface, KeeperAvatarService>();
            builder.Services.AddScoped<CompanyImageInterface, CompanyImageService>();
            builder.Services.AddScoped<QuestionnaireInterface, QuestionnaireService>();
            await builder.Build().RunAsync();
        }
    }
}
