using CookieAuthentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;

namespace CookieAuthentication.Services.ServerServices
{
    public class UserAuthService
    {
        public void SignIn(int UserId, string Name, string Email, List<string> roles, IOwinContext context)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, Name));
            claims.Add(new Claim(ClaimTypes.Email, Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(UserId)));

            for (int i = 0; i < roles.Count; i++)
            {
                claims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            var id = new ClaimsIdentity(claims,
                                        DefaultAuthenticationTypes.ApplicationCookie);

            var ctx = context;
            var authenticationManager = ctx.Authentication;
            AuthenticationProperties props = new AuthenticationProperties
            {
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddDays(30),
                AllowRefresh = true,

            };
            authenticationManager.SignIn(props, id);

        }

        public void SignOut(IOwinContext owinContext)
        {
            IEnumerable<AuthenticationDescription> authenticationTypes = owinContext.Authentication.GetAuthenticationTypes();
            owinContext.Authentication.SignOut(authenticationTypes.Select(o => o.AuthenticationType).ToArray());
        }

        public UserAuthModel CurrentUser()
        {
            bool IsAuthenticated = HttpContext.Current.User.Identity.IsAuthenticated;

            if (IsAuthenticated == true)
            {
                ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;
                List<string> roles = null;
                UserAuthModel user = new UserAuthModel();

                foreach (var claim in claimsIdentity.Claims)
                {
                    switch (claim.Type)
                    {
                        case ClaimTypes.NameIdentifier:
                            int id = 0;

                            if (Int32.TryParse(claim.Value, out id))
                            {
                                user.Id = id;
                            }

                            break;
                        case ClaimTypes.Name:
                            user.Name = claim.Value;
                            break;

                        case ClaimTypes.Email:
                            user.Email = claim.Value;
                            break;

                        case ClaimTypes.Role:
                            if (roles == null)
                            {
                                roles = new List<string>();
                            }

                            roles.Add(claim.Value);

                            break;

                        default:
                            break;
                    }

                }

                user.Roles = roles;

                return user;
            }
            return null;
        }
    }
}