using ClothingStore.Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _keyAccessToken;
        private readonly SymmetricSecurityKey _keyRefreshToken;
        public TokenService(IConfiguration configuration)
        {
            _keyAccessToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _keyRefreshToken = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["RefreshKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Username),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.role.name),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };

            var creds = new SigningCredentials(_keyAccessToken, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(30),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string CreateTokenRefresh(User user)
        {
            var signingCredentials =
                new SigningCredentials(_keyRefreshToken, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload(user.Username, null, null, null, DateTime.Today.AddDays(5)); // The expired time of payload is 5 days
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken GetPayloadRefreshToken(string refreshToken)
        {
            var handler = new JwtSecurityTokenHandler();

            handler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _keyRefreshToken,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);

            return validatedToken as JwtSecurityToken;
        }
    }
}
