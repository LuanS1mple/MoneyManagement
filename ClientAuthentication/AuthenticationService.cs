using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClientAuthentication
{
    public static class AuthenticationService
    {
        public static bool isValidToken(string accessToken, string IssuerSigningKey,string issuer, out SecurityToken? token,out ClaimsPrincipal? claimsPrincipal)
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                //ValidateAudience = true,
                //ValidAudience = aud,

                ValidateIssuer = true,
                ValidIssuer = issuer,
            };
            try
            {
                claimsPrincipal = securityTokenHandler.ValidateToken(accessToken, parameters, out token);
                return true;
            }
            catch (Exception ex)
            {
                claimsPrincipal=null;
                token = null;
                return false;
            }
        }
    }
}
