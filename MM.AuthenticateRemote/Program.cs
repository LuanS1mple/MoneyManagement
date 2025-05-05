
using ClientAuthentication;

namespace MM.AuthenticateRemote
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            builder.Services.AddAuthentication("ClientAuthentication").AddScheme<ClientAuthenticationHandlerOption, ClientAuthenticaitonHandler>("ClientAuthentication", option =>
            {
                option.IssuerSigningKey = builder.Configuration["IssuerSigningKey"]!;
                option.TimeRefresh = 30;
                option.Issuer = "LuanS1mple";
            });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
