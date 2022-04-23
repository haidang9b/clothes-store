﻿using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class StoreRepository: IStoreRepository
    {
        private readonly ClothingContext _dbContext;
        public StoreRepository(ClothingContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Store>> GetStores()
        {
            return await _dbContext.stores.ToListAsync() ;
        }

        public async Task<Store> GetStoreByID(int id)
        {
            return await _dbContext.stores.FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task<bool> InsertStore(Store store)
        {
            _dbContext.stores.Add(store);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> EditStore(Store store)
        {
            var old = await _dbContext.stores.FirstOrDefaultAsync(s => s.id == store.id);
            if (old == null)
            {
                return false;
            }
            old.name = store.name;
            old.phoneNumber = store.phoneNumber;
            old.address = store.address;
            var row = await _dbContext.SaveChangesAsync();
            return row >0;
        }

        public async Task<bool> DeleteStore(Store store)
        {
            var old = await _dbContext.stores.FirstOrDefaultAsync(s => s.id == store.id);
            if (old == null)
            {
                return false;
            }
            _dbContext.stores.Remove(old);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }
    }
}
