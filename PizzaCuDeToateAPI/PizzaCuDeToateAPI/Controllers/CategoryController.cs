using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Repositories.CategoryRepository;

namespace PizzaCuDeToateAPI.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CategoryController : Controller
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryController(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<Category> GetCategories()
    {
        if (true)
        {
            return Ok(_categoryRepository.GetAll());
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpGet("id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    
    public ActionResult<Category> GetCategoryById(int id)
    {
        if (true)
        {
            return Ok(_categoryRepository.GetSingle(category => category.Id == id));
        }
        else
        {
            return BadRequest();
        }
        
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<Category> AddCategory(Category categoryToAdd)
    {
        if (true)
        {
            return Ok(_categoryRepository.AddSingle(categoryToAdd));
        }
        else
        {
            return BadRequest() ;
        }
    }

    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<bool> UpdateOneCategory(Category categoryUpdated)
    {
        if (true)
        {
            return Ok(_categoryRepository.UpdateSingle(categoryUpdated));
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
        _categoryRepository.DeleteAll();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public ActionResult<Category> DeleteSingleCategory(Category categoryToDelete)
    {
        if (true)
        {
            return Ok(_categoryRepository.DeleteSingle(categoryToDelete));
        }
        else
        {
            return BadRequest();
        }
    }
}