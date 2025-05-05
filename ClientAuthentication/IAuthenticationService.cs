using Entities.Models;
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
    public interface IAuthenticationService
    {
        public  bool isValidToken(string accessToken, string IssuerSigningKey, string issuer, out SecurityToken? token, out ClaimsPrincipal? claimsPrincipal,string refreshToken);
        public bool IsValidRefreshToken(string refreshToken);

        public  string CreateNewRefreshToken(int time,string oldToken);
        public void DeleteRefreshToken(string token);
        public string RenewAccessToken(ClaimsPrincipal claimsPrincipal,string key,string aud);
        public bool isValidTokenIgnoreTime(string accessToken, string IssuerSigningKey, string issuer, out SecurityToken? token, out ClaimsPrincipal? claimsPrincipal, string refreshToken);
        public Customer GetByRefreshToken(string refreshToken);
        public string GetAccessTokenByCustomer(Customer customer,string key);
        public bool IsTokenValid(string token,string key, string iss);
    }
}
