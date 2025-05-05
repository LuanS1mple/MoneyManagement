
using ClientAuthentication;

namespace MM.AuthenticationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAuthentication("ClientAuthentication").AddScheme<ClientAuthenticationHandlerOption, ClientAuthenticaitonHandler>("ClientAuthentication", option =>
            {
                option.IssuerSigningKey = builder.Configuration["IssuerSigningKey"];
                option.TimeRefresh = 30;
                option.Issuer = "LuanS1mple";
            });
            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            app.MapControllers();

            app.Run();
        }
    }
}
