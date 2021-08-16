using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.JWTAuth.Entities
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public int TokenExp { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
