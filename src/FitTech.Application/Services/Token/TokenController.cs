using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FitTech.Application.Services.Token
{
    public class TokenController
    {
        private const string EMAIL_ALIAS = "eml";
        private const string USER_TYPE_ALIAS = "ust";
        private readonly double _tokenValidTimeInMinutes;
        private readonly string _securityKey;

        public TokenController(double tokenValidTimeInMinutes, string securityKey)
        {
            _tokenValidTimeInMinutes = tokenValidTimeInMinutes;
            _securityKey = securityKey;
        }

        public string GenerateToken(string userEmail, string userType)
        {
            var claims = new List<Claim>
            {
                new Claim(EMAIL_ALIAS, userEmail),
                new Claim(USER_TYPE_ALIAS, userType)

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

        public (string userEmail, string userType) RecoverUser(string token)
        {
            var claims = ValidToken(token);

            var email = claims.FindFirst(EMAIL_ALIAS).Value;
            var type = claims.FindFirst(USER_TYPE_ALIAS).Value;

            return (email, type);
        }

        private SymmetricSecurityKey SimetricKey()
        {
            var symetricKey = Convert.FromBase64String(_securityKey);
            return new SymmetricSecurityKey(symetricKey);
        }
    }
}
