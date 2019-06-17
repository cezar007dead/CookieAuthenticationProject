using CookieAuthentication.Models;
using CookieAuthentication.Services.ServerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace CookieAuthentication.Controllers
{
    [RoutePrefix("api/user")]
    public class UserAuthApiController : ApiController
    {
        private UserAuthService _userService;
        public UserAuthApiController()
        {
            _userService = new UserAuthService();
        }
        [HttpPost]
        [Route("login")]
        public HttpResponseMessage Login()
        {
            //Here you should do your vallidation 
            var ctx = Request.GetOwinContext();
            List<string> roles = new List<string>();
            roles.Add("Admin");
            _userService.SignIn(1, "Brock", "brockallen@gmail.com", roles, ctx);
            return Request.CreateResponse(HttpStatusCode.OK, "Loged in!");
        }
        [HttpPost]
        [Route("logout")]
        public HttpResponseMessage Logout()
        {
            _userService.SignOut(HttpContext.Current.Request.GetOwinContext());

            return Request.CreateResponse(HttpStatusCode.OK, "Loged out!");
        }
        [HttpGet]
        [Authorize]
        [Route("current")]
        public HttpResponseMessage Current()
        {
            UserAuthModel currentUser = _userService.CurrentUser();

            return Request.CreateResponse(HttpStatusCode.OK, currentUser);
        }
    }
}
