using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ClothingContext _dbContext;
        public CustomerRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }
        public async Task<bool> DeleteCustomer(Customer customer)
        {
            var old = await _dbContext.customers.FirstOrDefaultAsync(c => c.id == customer.id);
            if (old == null)
            {
                return false;
            }
            _dbContext.customers.Remove(old);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> EditCustomer(Customer customer)
        {
            var old = await _dbContext.customers.FirstOrDefaultAsync(c => c.id == customer.id);
            if (old == null)
            {
                return false;
            }
            old.phoneNumber = customer.phoneNumber;
            old.name = customer.name;
            old.address = customer.address;
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(c => c.id == id);
        }

        public async Task<Customer> GetCustomerByNumberPhone(string numberPhone)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(c => c.phoneNumber == numberPhone);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _dbContext.customers.ToListAsync();
        }

        public async Task<bool> InsertCustomer(Customer customer)
        {
            _dbContext.customers.Add(customer);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
