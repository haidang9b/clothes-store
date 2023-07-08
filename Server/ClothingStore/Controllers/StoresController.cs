using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        public StoresController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        // GET: api/<StoresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _storeRepository.GetStores();
                result.Message = "Get stores is successfully";
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // GET api/<StoresController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = new ApiResult();
            try
            {
                result.Data = await _storeRepository.GetStoreByID(id);
                result.Message = "Get a store is successfully";
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // POST api/<StoresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Store value)
        {
            var result = new ApiResult();
            try
            {
                if (await _storeRepository.InsertStore(value))
                {
                    result.IsSuccess = true;
                    result.Message = "Insert a store is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Insert a store is failed";
                }
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // PUT api/<StoresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Store value)
        {
            var result = new ApiResult();
            try
            {
                value.Id = id;
                if (await _storeRepository.EditStore(value))
                {
                    result.IsSuccess = true;
                    result.Message = "Update a store is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Update a store is failed";
                }
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // DELETE api/<StoresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = new ApiResult();
            try
            {
                var store = new Store { Id = id };
                if (await _storeRepository.DeleteStore(store))
                {
                    result.Message = "Delete a store is successfully";
                    result.IsSuccess = true;
                }
                else
                {
                    result.Message = "Delete a store is failed";
                    result.IsSuccess = false;
                }
            }
            catch
            {
                result.InternalError();
            }
            return Ok(result);
        }
    }
}
