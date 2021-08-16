using IorwoodDemo.Areas.JWTAuth.Entities;
using IorwoodDemo.Areas.JWTAuth.Helpers.Abstract;
using IorwoodDemo.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IorwoodDemo.Areas.JWTAuth.Helpers
{
    public class JwtAuthManager : IJwtAuthManager
    {
        public IImmutableDictionary<string, RefreshToken> UsersRefreshTokensReadOnlyDictionary => _userRefreshTokens.ToImmutableDictionary();
        private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshTokens;
        private readonly JwtTokenConfig _jwtTokenConfig;
       
             
        private readonly byte[] _secret;


        public JwtAuthManager(JwtTokenConfig jwtTokenConfig)
        {
          
            _jwtTokenConfig = jwtTokenConfig;
            _userRefreshTokens = new ConcurrentDictionary<string, RefreshToken>();
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);

        }

       public JwtAuthResult GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            try
            {
                var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Aud)?.Value);

                var jwtToken = new JwtSecurityToken(_jwtTokenConfig.Issuer, 
                    shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty, 
                    claims,
                    expires:now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                    signingCredentials:new Microsoft.IdentityModel.Tokens.SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));

                var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                var refreshToken = new RefreshToken()
                {
                    UserName = username,
                    TokenString = GenerateRefreshTokenString(),
                    ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration)
                };

                _userRefreshTokens.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

                return new JwtAuthResult()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    UserName = username
                    
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
        private static string GenerateRefreshTokenString()
        {
            var randomNumber = new byte[10];
            using var randomNumberGenerater = RandomNumberGenerator.Create();
            randomNumberGenerater.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }


        public JwtAuthResult Refresh(string refreshToken, string accessToken, DateTime now)
        {

            var (principal, jwtToken) = DecodeJwtToken(accessToken);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
                 throw new SecurityTokenException("Invalid Token");
               



            var userName = principal.Identity?.Name;

            if(!_userRefreshTokens.TryGetValue(refreshToken, out var existingRefreshToken))
                throw new SecurityTokenException("Invalid Token");
            //return new JwtAuthResult { AccessToken = null };

            if (existingRefreshToken.UserName != userName.ToString() || existingRefreshToken.ExpireAt < now)
                throw new SecurityTokenException("Invalid Token");

            return GenerateTokens(userName.ToString(), principal.Claims.ToArray(), DateTime.Now);
        }


     
        public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new SecurityTokenException("Invalid token");
                }


                var principal = new JwtSecurityTokenHandler()
                    .ValidateToken(token,
                        new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidIssuer = _jwtTokenConfig.Issuer,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(_secret),
                            ValidAudience = _jwtTokenConfig.Audience,
                            ValidateAudience = true,
                            ValidateLifetime = false,
                            ClockSkew = TimeSpan.FromMinutes(1)
                        },
                        out var validatedToken);
                return (principal, validatedToken as JwtSecurityToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RemoveExpiredRefreshTokens(DateTime now)
        {
            var expiredTokens = _userRefreshTokens.Where(u => u.Value.ExpireAt < now).ToList();
            foreach (var item in expiredTokens)
            {
                _userRefreshTokens.TryRemove(item.Key, out _);
            }
        }

        public void RemoveRefreshTokenByUsername(string userName)
        {
            var refreshTokens = _userRefreshTokens.Where(x => x.Value.UserName == userName).ToList();
            foreach (var refreshToken in refreshTokens)
            {
                _userRefreshTokens.TryRemove(refreshToken.Key, out _);
            }
        }


    }
}
