using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.JWTAuth.Entities
{
    public class JwtAuthResult : IActionResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public RefreshToken RefreshToken { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
