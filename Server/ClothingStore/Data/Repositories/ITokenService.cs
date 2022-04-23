using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
