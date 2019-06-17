using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CookieAuthentication.Models
{
    public class UserAuthModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; }

    }
}