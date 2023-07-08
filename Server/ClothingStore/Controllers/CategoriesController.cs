using ClothingStore.Data.Repositories;
using ClothingStore.Entities;
using ClothingStore.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClothingStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            ApiResult result = new ApiResult();
            try
            {
                result.Data = await _categoryRepository.GetCategories();
            }
            catch (Exception ex)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            ApiResult result = new ApiResult();
            try
            {
                var category = await _categoryRepository.GetCategoryByID(id);
                if (category == null)
                {
                    result.Message = "Get category by ID is failed";
                    result.IsSuccess = false;
                }
                else
                {
                    result.Data = category;
                    result.IsSuccess = true;
                    result.Message = "Get category by ID is Successfully";
                }
            }
            catch (Exception e)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // POST api/<CategoriesController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Category category)
        {
            ApiResult result = new ApiResult();
            try
            {
                if (await _categoryRepository.InsertCategory(category))
                {
                    result.Message = "Add new category is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Add new category is failed";
                }
            }
            catch (Exception ex)
            {
                result.InternalError();
            }
            return Ok(result);

        }

        // PUT api/<CategoriesController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] Category category)
        {
            ApiResult result = new ApiResult();
            try
            {
                category.Id = id;
                if (await _categoryRepository.EditCategory(category))
                {
                    result.Message = "Update category is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Update category is failed";
                }
            }
            catch (Exception ex)
            {
                result.InternalError();
            }
            return Ok(result);
        }

        // DELETE api/<CategoriesController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ApiResult result = new ApiResult();
            try
            {
                var category = new Category { Id = id };
                if (await _categoryRepository.DeleteCategory(category))
                {
                    result.Message = "Remove a category is successfully";
                }
                else
                {
                    result.IsSuccess = false;
                    result.Message = "Remove a category is failed";
                }
            }
            catch (Exception ex)
            {
                result.InternalError();
            }
            return Ok(result);
        }
    }
}
