using ClothingStore.Entities.Models;

namespace ClothingStore.Data.Repositories
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
