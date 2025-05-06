using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace MM.HostApp
{
    public class AuthenticationSchemaNothing: AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public AuthenticationSchemaNothing(IOptionsMonitor<AuthenticationSchemeOptions> options,
                            ILoggerFactory logger,
                            UrlEncoder encoder,
                            ISystemClock clock)
        : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Không xác thực gì cả, để middleware xử lý
            return Task.FromResult(AuthenticateResult.NoResult());
        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // Redirect khi chưa đăng nhập (401)
            Context.Response.Redirect("/home/loggin");
            return Task.CompletedTask;
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            // Redirect khi không có quyền truy cập (403)
            Context.Response.Redirect("/home/loggin");
            return Task.CompletedTask;
        }
    }
}
