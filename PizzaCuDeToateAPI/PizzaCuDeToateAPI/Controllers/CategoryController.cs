using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.CategoryRepository;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = _categoryRepository.GetAll();
            if (result.Count() == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var result = _categoryRepository.GetSingle(category => category.Id == id);
            if (result is null)
            {
                return NotFound($"Couldn't find category with id {id} in database!");
            }
            return Ok(result);
        }
        
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetCategoryByName([FromRoute] string name)
        {
            var result = _categoryRepository.GetSingle(category => category.Name == name);
            if (result is null)
            {
                return NotFound($"Couldn't find category with name {name} in database!");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCategory(CategoryDTO request)
        {
            // var findCategory = _categoryRepository.GetSingle(category => category.Name == request.Name);
            // if (findCategory is not null)
            // {
            //     return BadRequest("Category already exists!");
            // }
            var categoryToAdd = new Category();
            categoryToAdd.Name = request.Name;
            categoryToAdd.Description = request.Description;
            categoryToAdd.Logo = request.Logo;
            var result = _categoryRepository.AddSingle(categoryToAdd);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!");
            }
            return Ok(result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategoryById([FromRoute] int id, [FromBody] CategoryDTO request)
        {
            var findCategory = _categoryRepository.GetSingle(category => category.Id == id);
            if (findCategory is null)
            {
                return NotFound($"Couldn't find category with id {id} in database!");
            }
            var updatedCategory = new Category(id, request.Name, request.Description, request.Logo);
            var result = _categoryRepository.UpdateSingle(findCategory,updatedCategory);
            if (!result)
            {
                return StatusCode(500, "Server error, please try again later!");
            }
            return Ok(updatedCategory);
        }

        [HttpDelete]
        [Route("all")]
        public async Task<IActionResult> DeleteAllCategories()
        {
            try
            {
                _categoryRepository.DeleteAll();
                return Ok("Deleted all categories!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error, please try again later!"); 
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] int id)
        {
            var findCategory = _categoryRepository.GetSingle(category => category.Id == id);
            if (findCategory is null)
            {
                return NotFound($"Couldn't find category with id {id} in database!");
            }
            var result = _categoryRepository.DeleteSingle(findCategory);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!"); 
            }
            return Ok(result);
        }
    }
}
