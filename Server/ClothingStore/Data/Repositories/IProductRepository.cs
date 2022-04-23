using ClothingStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsByCategoryID(int id);
        Task<Product> GetProductByID(int id);
        Task<bool> InsertProduct(Product product);
        Task<bool> EditProduct(Product product);
        Task<bool> DeleteProduct(Product product);
        Task<IEnumerable<Product>> GetNewArrivals(int pageSize);
    }
}
