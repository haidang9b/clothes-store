using ClothingStore.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStores();
        Task<Store> GetStoreByID(int id);
        Task<bool> InsertStore(Store store);
        Task<bool> EditStore(Store store);
        Task<bool> DeleteStore(Store store);
    }
}
