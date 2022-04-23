using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = await _repository.GetCustomers();
                result.Message = "Get all customer is successfully";
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = await _repository.GetCustomerByID(id);
                result.Message = "Get customer by ID is successfully";
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpGet("get-by-phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = await _repository.GetCustomerByNumberPhone(phone);
                result.Message = "Get customer by phone is successfully";
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Customer value)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (await _repository.InsertCustomer(value))
                {
                    result.Message = "Add new customer is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Add new customer is failed";
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Customer customer)
        {
            ApiResult result = new ApiResult();
            try
            {
                customer.id = id;
                if (await _repository.EditCustomer(customer))
                {
                    result.Message = "Update customer is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Update customer is failed";
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ApiResult result = new ApiResult();
            try
            {
                var old = new Customer { id = id };
                if (await _repository.DeleteCustomer(old))
                {
                    result.Message = "Remove a customer is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Remove a customer is failed";
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }
    }
}
