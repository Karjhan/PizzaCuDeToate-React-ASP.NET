using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.CategoryRepository;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;
using PizzaCuDeToateAPI.Repositories.StockItemRepository;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly IFoodItemRepository _foodItemRepository;
        public FoodItemController(IFoodItemRepository foodItemRepository)
        {
            _foodItemRepository = foodItemRepository;
        }
        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllFoodItems()
        {
            var result = _foodItemRepository.GetAll();
            if (result.Count() == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
        
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetFoodItemById([FromRoute] int id)
        {
            var result = _foodItemRepository.GetSingle(foodItem => foodItem.Id == id);
            if (result is null)
            {
                return NotFound($"Couldn't find food item with id {id} in database!");
            }
            return Ok(result);
        }
        
        [HttpGet("getByName/{name}")]
        public async Task<IActionResult> GetFoodItemByName([FromRoute] string name)
        {
            var result = _foodItemRepository.GetSingle(foodItem => foodItem.Name == name);
            if (result is null)
            {
                return NotFound($"Couldn't find food item with name {name} in database!");
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddFoodItem(AddFoodItemDTO request)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Name == request.Name);
            if (findFoodItem is not null)
            {
                return BadRequest("Food item already exists!");
            }
            var foodItemToAdd = new FoodItem();
            foodItemToAdd.Name = request.Name;
            foodItemToAdd.Description = request.Description;
            foodItemToAdd.Logo = request.Logo;
            foodItemToAdd.Images = request.Images;
            foodItemToAdd.UnitPrice = request.UnitPrice;
            foodItemToAdd = _foodItemRepository.ChangeCategory(foodItemToAdd,request.CategoryId);
            if (foodItemToAdd is null)
            {
                return BadRequest("Incorrect id for category!");
            }
            foreach (var id in request.IngredientsIds)
            {
                foodItemToAdd = _foodItemRepository.AddIngredient(foodItemToAdd, id);
                if (foodItemToAdd is null)
                {
                    return BadRequest($"Either stock item with id {id} doesn't exist, or it's not an ingredient!");
                }
            }
            var result = _foodItemRepository.AddSingle(foodItemToAdd);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!");
            }
            return Ok(result);
        }
        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFoodItemById([FromRoute] int id, [FromBody] UpdateFoodItemDTO request)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == id);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {id} in database!");
            }
            var updatedFoodItem = new FoodItem();
            updatedFoodItem.Id = id;
            updatedFoodItem.Name = request.Name;
            updatedFoodItem.Description = request.Description;
            updatedFoodItem.Logo = request.Logo;
            updatedFoodItem.UnitPrice = request.UnitPrice;
            updatedFoodItem.Images = findFoodItem.Images;
            updatedFoodItem.Ingredients = findFoodItem.Ingredients;
            var result = _foodItemRepository.UpdateSingle(findFoodItem,updatedFoodItem);
            if (!result)
            {
                return StatusCode(500, "Server error, please try again later!");
            }
            return Ok(updatedFoodItem);
        }
        
        [HttpDelete]
        [Route("all")]
        public async Task<IActionResult> DeleteAllFoodItems()
        {
            try
            {
                _foodItemRepository.DeleteAll();
                return Ok("Deleted all food items!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error, please try again later!"); 
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFoodItemById([FromRoute] int id)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == id);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {id} in database!");
            }
            var result = _foodItemRepository.DeleteSingle(findFoodItem);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!"); 
            }
            return Ok(result);
        }
        
        [HttpPut("updateCategory/{foodItemId}/{categoryId}")]
        public async Task<IActionResult> UpdateFoodItemCategory([FromRoute] int foodItemId, [FromRoute] int categoryId)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == foodItemId);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {foodItemId} in database!");
            }
            findFoodItem = _foodItemRepository.ChangeCategory(findFoodItem, categoryId);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find category with id {categoryId} in database!");
            }
            return Ok(findFoodItem);
        }
        
        [HttpPut("addImage/{id}")]
        public async Task<IActionResult> AddImage([FromRoute] int id, [FromBody] ImageDTO request)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == id);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {id} in database!");
            }
            _foodItemRepository.AddImage(findFoodItem,request.ImagePath);
            return Ok();
        }
        
        [HttpPut("removeImage/{id}")]
        public async Task<IActionResult> RemoveImage([FromRoute] int id, [FromBody] ImageDTO request)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == id);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {id} in database!");
            }
            _foodItemRepository.RemoveImage(findFoodItem,request.ImagePath);
            return Ok();
        }
        
        [HttpPut("addIngredient/{foodItemId}/{ingredientId}")]
        public async Task<IActionResult> AddIngredient([FromRoute] int foodItemId, [FromRoute] int ingredientId)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == foodItemId);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {foodItemId} in database!");
            }
            findFoodItem = _foodItemRepository.AddIngredient(findFoodItem, ingredientId);
            if (findFoodItem is null)
            {
                return BadRequest($"Couldn't find food ingredient with id {ingredientId} in database, or it's not an ingredient!");
            }
            return Ok(findFoodItem);
        }
        
        [HttpPut("removeIngredient/{foodItemId}")]
        public async Task<IActionResult> RemoveIngredient([FromRoute] int foodItemId, [FromRoute] int ingredientId)
        {
            var findFoodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Id == foodItemId);
            if (findFoodItem is null)
            {
                return NotFound($"Couldn't find food item with id {foodItemId} in database!");
            }
            findFoodItem = _foodItemRepository.RemoveIngredient(findFoodItem, ingredientId);
            if (findFoodItem is null)
            {
                return BadRequest($"Couldn't find food ingredient with id {ingredientId} in database, or it's not an ingredient!");
            }
            return Ok(findFoodItem);
        }
    }
}
