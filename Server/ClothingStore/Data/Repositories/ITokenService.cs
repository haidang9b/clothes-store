using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface ITokenService
    {
        public string CreateToken(User user);
        public string CreateTokenRefresh(User user);
        public JwtSecurityToken GetPayloadRefreshToken(string refreshToken);
    }
}
