using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.FoodItemRepository;

namespace PizzaCuDeToateAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class FoodItemController : Controller
{
    
    private readonly FoodItemRepository _foodItemRepository;

    public FoodItemController(FoodItemRepository foodItemRepository)
    {
        _foodItemRepository = foodItemRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<FoodItem> GetFoodItems()
    {
        if (true)
        {
            return Ok(_foodItemRepository.GetAll());
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public ActionResult<FoodItem> GetFoodItemById(int id)
    {
        if (true)
        {
            return Ok(_foodItemRepository.GetSingle(category => category.Id == id));
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<FoodItem> AddFoodItem(FoodItem foodItemToAdd)
    {
        if (true)
        {
            return Ok(_foodItemRepository.AddSingle(foodItemToAdd));
        }
        else
        {
            return BadRequest() ;
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<bool> UpdateOneFoodItem(FoodItem foodItemUpdated)
    {
        if (true)
        {
            return Ok(_foodItemRepository.UpdateSingle(foodItemUpdated));
        }
        else
        {
            return BadRequest();
        }
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public void DeleteAll()
    {
        _foodItemRepository.DeleteAll();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<FoodItem> DeleteSingleCategory(FoodItem foodItemToDelete)
    {
        if (true)
        {
            return Ok(_foodItemRepository.DeleteSingle(foodItemToDelete));
        }
        else
        {
            return BadRequest();
        }
    }
}