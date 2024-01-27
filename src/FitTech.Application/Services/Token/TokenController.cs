﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FitTech.Application.Services.Token
{
    public class TokenController
    {
        private const string EMAIL_ALIAS = "eml";
        private readonly int _tokenValidTimeInMinutes;
        private readonly string _securityKey;

        public TokenController(int tokenValidTimeInMinutes, string securityKey)
        {
            _tokenValidTimeInMinutes = tokenValidTimeInMinutes;
            _securityKey = securityKey;
        }

        public string GenerateToken(string userEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(EMAIL_ALIAS, userEmail)

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenValidTimeInMinutes),
                SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

        public ClaimsPrincipal ValidToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                IssuerSigningKey = SimetricKey(),
                ClockSkew = new TimeSpan(0),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            var claims = tokenHandler.ValidateToken(token, validationParameters, out _);

            return claims;
        }

        private SymmetricSecurityKey SimetricKey()
        {
            var symetricKey = Convert.FromBase64String(_securityKey);
            return new SymmetricSecurityKey(symetricKey);
        }
    }
}