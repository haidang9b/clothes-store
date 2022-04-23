using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ApiResult();
            try
            {
                result.Message = "Get Products is successfully";
                result.Data = await _repository.GetProducts();
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _repository.GetProductByID(id);
                if (result.Data == null)
                {
                    result.Message = "Not exist product with this id";
                    result.IsSuccess = false;
                }
                else
                {
                    result.Message = "Get Product is successfully";
                    result.IsSuccess = true;
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // POST api/<ProductsController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Product value)
        {
            var result = new ApiResult();
            try
            {
                if (await _repository.InsertProduct(value))
                {
                    result.Message = "Add a product is successfully";
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Add a product is failed";
                    result.IsSuccess = false;
                }
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // PUT api/<ProductsController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Product product)
        {
            var result = new ApiResult();
            
            try
            {
                product.id = id;
                if (await _repository.EditProduct(product))
                {
                    result.Message = "Update a product is successfully";
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Update a product is failed";
                    result.IsSuccess = false;
                }
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // DELETE api/<ProductsController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResult();
            try
            {
                var product = new Product { id = id };
                if (await _repository.DeleteProduct(product))
                {
                    result.Message = "Delete a product is successfully";
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Delete a product is failed";
                    result.IsSuccess = false;
                }
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpGet("new-arrivals/{page}")]
        public async Task<IActionResult> GetNewArrivals(int page)
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _repository.GetNewArrivals(page);
                result.Message = "GetNewArrivals is Successfully ";
            }
            catch(Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _repository.GetProductsByCategoryID(id);
                if (result.Data == null)
                {
                    result.Message = "Not exist products with this id";
                    result.IsSuccess = false;
                }
                else
                {
                    result.Message = "Get Products is successfully";
                    result.IsSuccess = true;
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
