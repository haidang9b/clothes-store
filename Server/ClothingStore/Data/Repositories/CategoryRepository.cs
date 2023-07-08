using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ClothingContext _dbContext;
        public CategoryRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }
        public async Task<bool> DeleteCategory(Category category)
        {
            var old = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (old == null)
            {
                return false;
            }
            _dbContext.categories.Remove(old);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> EditCategory(Category category)
        {
            var old = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (old == null)
            {
                return false;
            }
            old.Name = category.Name;
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _dbContext.categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByID(int id)
        {
            return await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> InsertCategory(Category category)
        {
            _dbContext.categories.Add(category);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
