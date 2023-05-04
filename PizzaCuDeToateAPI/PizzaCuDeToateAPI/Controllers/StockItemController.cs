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
    public class StockItemController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        private readonly IFoodItemRepository _foodItemRepository;

        private readonly ICategoryRepository _categoryRepository;

        public StockItemController(IStockRepository stockRepository, IFoodItemRepository foodItemRepository,ICategoryRepository categoryRepository)
        {
            _stockRepository = stockRepository;
            _foodItemRepository = foodItemRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("allStockItems")]

        public async Task<IActionResult> GetAllStockItems()
        {
            var result = _stockRepository.GetAll().ToList();
            if (result.Count == 0)
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpGet("getStockItemById/{id}")]

        public async Task<IActionResult> GetStockItemById([FromRoute] int id)
        {
            var result = _stockRepository.GetSingle(stockItem => stockItem.Id == id);
            if (result is null)
            {
                return NotFound($"Couldn't find stock item with id {id}");
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("addStockItem/{categoryName}")]

        public async Task<IActionResult> AddStockItem([FromRoute] string categoryName,StockItemDTO stockItemDto)
        {
            var stockItemToAdd = new StockItem();
            stockItemToAdd.Name = stockItemDto.Name;
            stockItemToAdd.UnitsInStock = stockItemDto.UnitsInStock;
            stockItemToAdd.IsIngredient = stockItemDto.IsIngredient;
            stockItemToAdd.QuantityPerUnit = stockItemDto.QuantityPerUnit;
            stockItemToAdd.Logo = stockItemDto.Logo;
            stockItemToAdd.UnitPrice = stockItemDto.UnitPrice;

            var category = _categoryRepository.GetSingle(category => category.Name == categoryName);
            if (category is null)
            {
                return NotFound($"Couldn't find category with name {categoryName}!");
            }

            stockItemToAdd.Category = category;
            
            var foodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Category.Id == category.Id);
            if (foodItem is null)
            {
                return NotFound($"Couldn't find food item with the category needed!");
            }
            stockItemToAdd.Meals.Add(foodItem);

            var result = _stockRepository.AddSingle(stockItemToAdd);

            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!");
            }

            return Ok(result);
        }
        
        
        [HttpPost]
        [Route("updateStockItem/{id}")]

        public async Task<IActionResult> UpdateStockItem([FromRoute] int id, StockItemDTO stockItemDto)
        {
            var stockItemToUpdate = _stockRepository.GetSingle(stockItem => stockItem.Id == id);
            if (stockItemToUpdate is null)
            {
                return NotFound($"Couldn't find stock item with id {id}!");
            }
            var stockItemUpdated = new StockItem();
            stockItemUpdated.Name = stockItemDto.Name;
            stockItemUpdated.UnitsInStock = stockItemDto.UnitsInStock;
            stockItemUpdated.IsIngredient = stockItemDto.IsIngredient;
            stockItemUpdated.QuantityPerUnit = stockItemDto.QuantityPerUnit;
            stockItemUpdated.Logo = stockItemDto.Logo;
            stockItemUpdated.UnitPrice = stockItemDto.UnitPrice;
            
            var foodItem = _foodItemRepository.GetSingle(foodItem => foodItem.Category.Id == stockItemToUpdate.Category.Id);
            if (foodItem is null)
            {
                return NotFound($"Couldn't find food item with the category needed!");
            }
            stockItemUpdated.Meals.Add(foodItem);

            var result = _stockRepository.UpdateSingle(stockItemToUpdate, stockItemUpdated);

            if (result is false)
            {
                return StatusCode(500, "Server error, please try again later!");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("deleteAllStockItems")]

        public async Task<IActionResult> DeleteAllStockItems()
        {
            try
            {
                _stockRepository.DeleteAll();
                return Ok("Deleted all stock items!");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error, please try again later!"); 
            }
        }

        [HttpDelete("deleteStockItem/{id}")]

        public async Task<IActionResult> DeleteStockItemById([FromRoute] int id, StockItem stockItemToDelete)
        {
            var stockItem = _stockRepository.GetSingle(stockItem => stockItem.Id == id);
            if (stockItem is null)
            {
                return NotFound($"Couldn't find stock item with id {id}");
            }

            var result = _stockRepository.DeleteSingle(stockItem);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!"); 

            }

            return Ok(result);
        }
            
            
           

    }
}
