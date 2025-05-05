using Entities.Models;
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
        private IAuthenticationService _authenticationService;

        public ClientAuthenticaitonHandler(IAuthenticationService authenticationService, IOptionsMonitor<ClientAuthenticationHandlerOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var refreshToken = Context.Request.Headers["RefreshToken"];
            //var accessToken = Context.Request.Headers["AccessToken"];
            if (/**string.IsNullOrWhiteSpace(accessToken) ||*/ string.IsNullOrEmpty(refreshToken))
            {
                return Task.FromResult(AuthenticateResult.Fail("Not enought token"));
            }
            //if (_authenticationService.isValidToken(accessToken, Options.IssuerSigningKey, Options.Issuer, out SecurityToken validatedToken, out ClaimsPrincipal claimsPrincipal, refreshToken))
            //{
            //    // renew lại accesstoken/refreshtoken
            //    _authenticationService.DeleteRefreshToken(refreshToken);
            //    string newRefreshToken = _authenticationService.CreateNewRefreshToken(Options.TimeRefresh);
            //    string newAcessToken = _authenticationService.RenewAccessToken(claimsPrincipal, Options.IssuerSigningKey,newRefreshToken);
            //    Context.Response.Cookies.Append("AccessToken", newAcessToken);
            //    Context.Response.Cookies.Append("RefreshToken", newRefreshToken);
            //    AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Scheme.Name);
            //    return Task.FromResult(AuthenticateResult.Success(ticket));
            //}
            //else
            //{
            if (!_authenticationService.IsValidRefreshToken(refreshToken))
            {
                return Task.FromResult(AuthenticateResult.Fail("Not enought token"));
            }
            else
            {
                _authenticationService.DeleteRefreshToken(refreshToken);
                Customer customer = _authenticationService.GetByRefreshToken(refreshToken);
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, customer.Name)
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name); // Scheme.Name là tên scheme của bạn
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            //}
        }
    }
}
