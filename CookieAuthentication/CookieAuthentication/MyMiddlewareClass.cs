using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CookieAuthentication
{
    public class MyMiddlewareClass : CookieAuthenticationProvider
    {


        public async Task ValidateIdentity(CookieValidateIdentityContext context)
        {
            return;
        }
    }


}