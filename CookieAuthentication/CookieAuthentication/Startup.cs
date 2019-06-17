using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(CookieAuthentication.Startup))]

namespace CookieAuthentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            CookieAuthenticationProvider provider = new CookieAuthenticationProvider();
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/"),
                CookieName = "authentication",
                SlidingExpiration = true,
                ExpireTimeSpan = new TimeSpan(30, 0, 0, 0),
                Provider = new CookieAuthenticationProvider
                {
                    OnApplyRedirect = context =>
                    {
                        context.Response.StatusCode = 401;

                    }
                }

            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}

