using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
