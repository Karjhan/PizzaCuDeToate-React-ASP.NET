using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.StockItemRepository;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockItemController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockItemController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Route("all")]

        public async Task<IActionResult> GetAllStockItems()
        {
            var result = _stockRepository.GetAll();
            if (result.Count() == 0)
            {
                return NoContent();
            }
            var final = result.Select(stockItem =>
            {
                var show = new JSONStockItemDTO();
                show.GetFromStockItem(stockItem);
                return show;
            });
            return Ok(final);
        }

        [HttpGet("getById/{id}")]

        public async Task<IActionResult> GetStockItemById([FromRoute] int id)
        {
            var result = _stockRepository.GetSingle(stockItem => stockItem.Id == id);
            if (result is null)
            {
                return NotFound($"Couldn't find stock item with id {id}");
            }
            var final = new JSONStockItemDTO();
            final.GetFromStockItem(result);
            return Ok(final);
        }


        [HttpPost]
        [Route("add")]

        public async Task<IActionResult> AddStockItem(StockItemDTO request)
        {
            var findStockItem = _stockRepository.GetSingle(stockItem => stockItem.Name == request.Name);
            if (findStockItem is not null)
            {
                return BadRequest("Stock item already exists!");
            }
            var stockItemToAdd = new StockItem();
            stockItemToAdd.Name = request.Name;
            stockItemToAdd.UnitsInStock = request.UnitsInStock;
            stockItemToAdd.IsIngredient = request.IsIngredient;
            stockItemToAdd.QuantityPerUnit = request.QuantityPerUnit;
            stockItemToAdd.Logo = request.Logo;
            stockItemToAdd.UnitPrice = request.UnitPrice;
            stockItemToAdd = _stockRepository.ChangeCategory(stockItemToAdd, request.CategoryId);
            if (stockItemToAdd.Category is null)
            {
                return NotFound($"Couldn't find category with id {request.CategoryId}!");
            }
            foreach (var id in request.MealIds)
            {
                stockItemToAdd = _stockRepository.AddMeal(stockItemToAdd, id);
                if (stockItemToAdd is null)
                {
                    return BadRequest($"Meal item with id {id} doesn't exist!");
                }
            }

            var result = _stockRepository.AddSingle(stockItemToAdd);
            if (result is null)
            {
                return StatusCode(500, "Server error, please try again later!");
            }
            var final = new JSONStockItemDTO();
            final.GetFromStockItem(result);
            return Ok(final);
        }
        
        
        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> UpdateStockItem([FromRoute] int id, [FromBody] UpdateStockItemDTO request)
        {
            var findStockItem = _stockRepository.GetSingle(stockItem => stockItem.Id == id);
            if (findStockItem is null)
            {
                return NotFound($"Couldn't find stock item with id {id}!");
            }
            var stockItemUpdated = new StockItem();
            stockItemUpdated.Id = id;
            stockItemUpdated.Name = request.Name;
            stockItemUpdated.UnitsInStock = request.UnitsInStock;
            stockItemUpdated.IsIngredient = request.IsIngredient;
            stockItemUpdated.QuantityPerUnit = request.QuantityPerUnit;
            stockItemUpdated.Logo = request.Logo;
            stockItemUpdated.UnitPrice = request.UnitPrice;
            stockItemUpdated.Category = findStockItem.Category;
            stockItemUpdated.Meals = findStockItem.Meals;
            
            var result = _stockRepository.UpdateSingle(findStockItem, stockItemUpdated);

            if (!result)
            {
                return StatusCode(500, "Server error, please try again later!");
            }

            var final = new JSONStockItemDTO();
            final.GetFromStockItem(stockItemUpdated);
            return Ok(final);
        }

        [HttpDelete]
        [Route("all")]

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

        [HttpDelete("delete/{id}")]

        public async Task<IActionResult> DeleteStockItemById([FromRoute] int id)
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
        
        [HttpPut("updateCategory/{stockItemId}/{categoryId}")]
        public async Task<IActionResult> UpdateStockItemCategory([FromRoute] int stockItemId, [FromRoute] int categoryId)
        {
            var findStockItem = _stockRepository.GetSingle(foodItem => foodItem.Id == stockItemId);
            if (findStockItem is null)
            {
                return NotFound($"Couldn't find food item with id {stockItemId} in database!");
            }
            findStockItem = _stockRepository.ChangeCategory(findStockItem, categoryId);
            if (findStockItem is null)
            {
                return NotFound($"Couldn't find category with id {categoryId} in database!");
            }
            var final = new JSONStockItemDTO();
            final.GetFromStockItem(findStockItem);
            return Ok(final);
        }
        
        [HttpPut("addMeal/{stockItemId}/{mealId}")]
        public async Task<IActionResult> AddMeal([FromRoute] int stockItemId, [FromRoute] int mealId)
        {
            var findStockItem = _stockRepository.GetSingle(stockItem => stockItem.Id == stockItemId);
            if (findStockItem is null)
            {
                return NotFound($"Couldn't find stock item with id {stockItemId} in database!");
            }
            findStockItem = _stockRepository.AddMeal(findStockItem, mealId);
            if (findStockItem is null)
            {
                return BadRequest($"Couldn't find meal with id {mealId} in database!");
            }
            var final = new JSONStockItemDTO();
            final.GetFromStockItem(findStockItem);
            return Ok(final);
        }
        
        [HttpPut("removeMeal/{stockItemId}/{mealId}")]
        public async Task<IActionResult> RemoveIngredient([FromRoute] int stockItemId, [FromRoute] int mealId)
        {
            var findStockItem = _stockRepository.GetSingle(stockItem => stockItem.Id == stockItemId);
            if (findStockItem is null)
            {
                return NotFound($"Couldn't find stock item with id {stockItemId} in database!");
            }
            findStockItem = _stockRepository.RemoveMeal(findStockItem, mealId);
            if (findStockItem is null)
            {
                return BadRequest($"Couldn't find meal with id {mealId} in database!");
            }
            var final = new JSONStockItemDTO();
            final.GetFromStockItem(findStockItem);
            return Ok(final);
        }
    }
}
