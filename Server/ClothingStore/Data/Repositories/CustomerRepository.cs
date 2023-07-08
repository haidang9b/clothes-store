using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var old = await _dbContext.customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
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
            var old = await _dbContext.customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
            if (old == null)
            {
                return false;
            }
            old.PhoneNumber = customer.PhoneNumber;
            old.Name = customer.Name;
            old.Address = customer.Address;
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> GetCustomerByNumberPhone(string numberPhone)
        {
            return await _dbContext.customers.FirstOrDefaultAsync(c => c.PhoneNumber == numberPhone);
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
