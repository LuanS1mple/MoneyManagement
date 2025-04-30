using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM.Usecase
{
    public interface IAuthentication
    {
        bool IsAuthenticatedByLogging(string username, string password);
        bool isValidAccessToken(string accessToken);
        bool IsValidRefreshToken(string refreshToken);
    }
}
