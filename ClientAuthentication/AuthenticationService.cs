using Azure.Core;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationService : IAuthenticationService
    {
        public bool isValidToken(string accessToken, string IssuerSigningKey, string issuer, out SecurityToken? token, out ClaimsPrincipal? claimsPrincipal, string refreshtoken)
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = false,
                ValidAudience = refreshtoken,

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
                claimsPrincipal = null;
                token = null;
                return false;
            }
        }
        public bool isValidTokenIgnoreTime(string accessToken, string IssuerSigningKey, string issuer, out SecurityToken? token, out ClaimsPrincipal? claimsPrincipal, string refreshtoken)
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IssuerSigningKey)),

                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,

                ValidateAudience = false,
                ValidAudience = refreshtoken,

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
                claimsPrincipal = null;
                token = null;
                return false;
            }
        }
        public bool IsValidRefreshToken(string refreshToken)
        {
            var repo = MoneyManagementContext.Ins;
            if (!repo.RefreshTokens.Where(s => s.Token.Equals(refreshToken)).Any()) { return false; }
            RefreshToken token = repo.RefreshTokens.Where(s => s.Token.Equals(refreshToken)).FirstOrDefault()!;
            if (!token.IsEnable)
            {
                return false;
            }
            if (token.ExpireTime < DateTime.UtcNow)
            {
                return false;
            }
            return true;
        }
        public string CreateNewRefreshToken(int timeRefresh,string oldToken)
        {
            var repo = MoneyManagementContext.Ins;
            string token = null;
            do
            {
                token = Guid.NewGuid().ToString().Substring(0, 8);
            }
            while (repo.RefreshTokens.Where(s => s.Token.Equals(token)).Any());
            RefreshToken refreshToken = new RefreshToken
            {
                Token = token,
                ExpireTime = DateTime.UtcNow.AddDays(timeRefresh),
                IsEnable = true,
                UserId = GetByRefreshToken(oldToken).Id
            };
            repo.RefreshTokens.Add(refreshToken);
            repo.SaveChanges();
            return token;
        }

        public void DeleteRefreshToken(string token)
        {
            var repo = MoneyManagementContext.Ins;
            var refreshToken = repo.RefreshTokens.Where(s => s.Token.Equals(token)).FirstOrDefault()!;
            refreshToken.IsEnable = false;
            repo.RefreshTokens.Update(refreshToken);
            repo.SaveChanges();
        }

        public string RenewAccessToken(ClaimsPrincipal claimsPrincipal, string key, string aud)
        {
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var newClaims = claimsPrincipal.Claims
                .Where(c => c.Type != JwtRegisteredClaimNames.Aud && c.Type != "aud")
                .ToList();


            var token = new JwtSecurityToken(
                claims: newClaims,
                expires: DateTime.UtcNow.AddMinutes(60),
                audience: aud,
                signingCredentials: creds
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Customer GetByRefreshToken(string refreshToken)
        {
            var repo = MoneyManagementContext.Ins;
            return repo.RefreshTokens.Include(s=>s.User).Where(s=>s.Token.Equals(refreshToken)).FirstOrDefault()!.User;
        }

        public string GetAccessTokenByCustomer(Customer customer,string key)
        {
            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(60),
                issuer: "LuanS1mple",
                audience: customer.Id.ToString(), 
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token) ;
        }

        public bool IsTokenValid(string token, string key, string iss)
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidateIssuer = true,
                ValidIssuer = iss,
            };
            try
            {
                var c = securityTokenHandler.ValidateToken(token,parameters,out SecurityToken st);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
