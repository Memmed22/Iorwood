using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace IorwoodDemo.Extensions
{
    public class CustomAuthentication  : AuthorizeAttribute
    {
        protected  void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadGateway,
                Content = new StringContent("You are unauthorized to access this resource"),
                
                
            };
        }
    }
}
