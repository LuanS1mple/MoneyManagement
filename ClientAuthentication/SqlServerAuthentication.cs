using MM.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAuthentication
{
    internal class SqlServerAuthentication : IAuthentication
    {
        public bool isValidAccessToken(string accessToken)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthenticatedByLogging(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsValidRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

    }
}
