using ClothingStore.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryByID(int id);
        Task<bool> InsertCategory(Category category);
        Task<bool> EditCategory(Category category);
        Task<bool> DeleteCategory(Category category);
    }
}
