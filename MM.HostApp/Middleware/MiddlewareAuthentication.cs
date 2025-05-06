using ClientAuthentication;
using Microsoft.IdentityModel.Tokens;
using MM.HostApp.Models;
using MM.HostApp.RemoteAuthencation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MM.HostApp.Middleware
{
    public class MiddlewareAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService authenticationService;

        public MiddlewareAuthentication(RequestDelegate next, IConfiguration configuration, IAuthenticationService  authenticationService)
        {
            _next = next;
            _configuration = configuration;
            this.authenticationService = authenticationService;
        }

        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Cookies["AccessToken"];
            var refreshToken = context.Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken) && string.IsNullOrEmpty(accessToken))
            {
                await _next(context);
            }
            if (!string.IsNullOrEmpty(accessToken))
            {
                // Giả sử bạn dùng JWT - bạn cần validate token ở đây
                bool isAuthenticated = authenticationService.IsTokenValid(accessToken, _configuration.GetSection("IssuerSigningKey").Value, "LuanS1mple", out ClaimsPrincipal principal);

                if (principal != null)
                {
                    context.User = principal;
                    await _next(context);

                }
            }
            else
            {
                if (string.IsNullOrEmpty(refreshToken))
                {
                    await _next(context);
                }
                else
                {
                    ResponseAuthentication? isAuthenticated = await RemoteApiAuthentication.IsAuthenticated("https://localhost:7242/", refreshToken);
                    if (isAuthenticated == null)
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.Cookies.Append("AccessToken", isAuthenticated.AccessToken);
                        context.Response.Cookies.Append("RefreshToken", isAuthenticated.RefreshToken);
                        bool convert = authenticationService.IsTokenValid(isAuthenticated.AccessToken, _configuration.GetSection("IssuerSigningKey").Value, "LuanS1mple", out ClaimsPrincipal principal);

                        if (principal != null)
                        {
                            context.User = principal;
                            await _next(context);
                        }
                    }
                }
                await _next(context);
            }
        }

    }
}
