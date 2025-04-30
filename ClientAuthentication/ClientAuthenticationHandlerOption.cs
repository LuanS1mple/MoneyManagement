using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAuthentication
{
    public class ClientAuthenticationHandlerOption : AuthenticationSchemeOptions
    {
        public  string IssuerSigningKey {  get;  } = string.Empty;
        public  string Issuer {  get;  } = string.Empty;
    }
}
