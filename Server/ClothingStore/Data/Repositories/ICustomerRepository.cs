using ClothingStore.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomerByID(int id);
        Task<Customer> GetCustomerByNumberPhone(string numberPhone);
        Task<bool> InsertCustomer(Customer customer);
        Task<bool> EditCustomer(Customer customer);
        Task<bool> DeleteCustomer(Customer customer);
    }
}
