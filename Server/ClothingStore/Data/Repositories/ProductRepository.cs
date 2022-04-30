using ClothingStore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ClothingContext _dbContext;
        public ProductRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }
        public async Task<bool> DeleteProduct(Product product)
        {
            var old = await _dbContext.products.Include(p => p.Category).FirstOrDefaultAsync(p => p.id == product.id);
            if (old == null)
            {
                return false;
            }
            _dbContext.products.Remove(old);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> EditProduct(Product product)
        {
            var old = await _dbContext.products.Where(p => p.id == product.id).FirstOrDefaultAsync();
            if (old == null)
            {
                return false;
            }
            old.quantity = product.quantity;
            old.image = product.image;
            old.price = product.price;
            old.title = product.title;
            old.category_id = product.category_id;
            old.description = product.description;
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<IEnumerable<Product>> GetNewArrivals(int pageSize)
        {
            return await _dbContext.products.OrderByDescending(p => p.id).Take(pageSize).ToListAsync();
        }

        public async Task<Product> GetProductByID(int id)
        {
            return await _dbContext.products.Include(p => p.Category).Where(p => p.id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _dbContext.products.Include(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryID(int id)
        {
            return await _dbContext.products.Include(p => p.Category).Where(p => p.category_id == id).ToListAsync();
        }

        public async Task<bool> InsertProduct(Product product)
        {
            _dbContext.products.Add(product);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
