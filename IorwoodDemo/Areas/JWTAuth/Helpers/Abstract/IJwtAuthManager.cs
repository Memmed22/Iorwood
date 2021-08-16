using IorwoodDemo.Areas.JWTAuth.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.JWTAuth.Helpers.Abstract
{
   public interface IJwtAuthManager
    {
        IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary { get; }
        JwtAuthResult  GenerateTokens(string username, Claim[] claims, DateTime now);
        JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now);
        
        void RemoveExpiredRefreshTokens(DateTime now);
        //Username ile ideal deyil, istifadeci iki browser isledirse, gedib diger browser sessionda  silecek
        void RemoveRefreshTokenByUsername(string userName);
        (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
    }
}
