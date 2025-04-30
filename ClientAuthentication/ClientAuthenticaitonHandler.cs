using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MM.Usecase;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ClientAuthentication
{
    public class ClientAuthenticaitonHandler : AuthenticationHandler<ClientAuthenticationHandlerOption>
    {
        private readonly IAuthentication authentication;
        public ClientAuthenticaitonHandler(IOptionsMonitor<ClientAuthenticationHandlerOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var refreshToken = Context.Request.Headers["RefreshToken"];
            var accessToken = Context.Request.Headers["AccessToken"];
            if(string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrEmpty(refreshToken){
                return AuthenticateResult.Fail("Not found enough Header required");
            }
            if (AuthenticationService.isValidToken(accessToken, Options.IssuerSigningKey,Options.Issuer, out SecurityToken validatedToken, out ClaimsPrincipal claimsPrincipal))
            {
                //authenticated user  
                JwtSecurityToken jwtSecurityToken = validatedToken as JwtSecurityToken;
                //thiếu renew lại accesstoken
                AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            if (!authentication.isValidAccessToken(accessToken))
            {
                if (!authentication.IsValidRefreshToken(refreshToken))
                {
                    return AuthenticateResult.Fail("AccessToken and RefreshToken is not valid");
                }
                else
                {
                    //create another accesstoken
                }
            }
        }
    }
}
