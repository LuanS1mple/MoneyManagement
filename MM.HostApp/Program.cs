using ClientAuthentication;
using Microsoft.AspNetCore.Authentication;
using MM.HostApp.Middleware;
using MM.Infrastructure;
using MM.Usecase;

namespace MM.HostApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<ICustomerRepository, SqlCustomerRepository>();
            builder.Services.AddSingleton<IJarRepository, SqlJarRepository>();
            builder.Services.AddSingleton<IUsageRepository, SqlUsageRepository>();
            builder.Services.AddSingleton<ITypeUsageRepository, SqlTypeUsageRepository>();
            builder.Services.AddSingleton<ClientAuthentication.IAuthenticationService, ClientAuthentication.AuthenticationService>();
            builder.Services.AddAuthentication("FakeScheme")
                 .AddScheme<AuthenticationSchemeOptions, AuthenticationSchemaNothing>("FakeScheme", 
                 options => {
                 });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<MiddlewareAuthentication>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
