using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.StockItemRepository;

namespace PizzaCuDeToateAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class StockItemController : Controller
{
    private readonly StockRepository _stockRepository;

    public StockItemController(StockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<StockItem> GetStockItems()
    {
        if (true)
        {
            return Ok(_stockRepository.GetAll());
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public ActionResult<StockItem> GetStockItemById(int id)
    {
        if (true)
        {
            return Ok(_stockRepository.GetSingle(stock => stock.Id == id));
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<StockItem> AddStockItem(StockItem stockItemToAdd)
    {
        if (true)
        {
            return Ok(_stockRepository.AddSingle(stockItemToAdd));
        }
        else
        {
            return BadRequest() ;
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<bool> UpdateOneStockItem(StockItem stockItem)
    {
        if (true)
        {
            return Ok(_stockRepository.UpdateSingle(stockItem));
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
        _stockRepository.DeleteAll();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<StockItem> DeleteSingleStockItem(StockItem stockItemToDelete)
    {
        if (true)
        {
            return Ok(_stockRepository.DeleteSingle(stockItemToDelete));
        }
        else
        {
            return BadRequest();
        }
    }
}