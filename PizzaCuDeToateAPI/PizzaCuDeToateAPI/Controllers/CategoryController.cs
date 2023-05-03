using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
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

    public ActionResult<Category> GetCategories()
    {
        var result = _categoryRepository.GetAll().ToList();
        if (result.Count==0)
        {
            return NotFound($"Categories not found!");
        }
        return Ok(result);
    }

    
    [HttpGet("id")]

    public ActionResult<Category> GetCategoryById(int id)
    {
        var result = _categoryRepository.GetSingle(category => category.Id == id);
        if (result is null)
        {
            return NotFound($"Couldn't find category with id {id} in database!");
        }
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Category> AddCategory(CategoryDTO categoryToAdd)
    {
        var newCategory = new Category();
        newCategory.Name = categoryToAdd.Name;
        newCategory.Description = categoryToAdd.Description;
        newCategory.Logo = categoryToAdd.Logo;
        var result = _categoryRepository.AddSingle(newCategory);
        if (result is null)
        {
            return NotFound($"Category with name {categoryToAdd.Name} can't be added into database!");
        }

        return Ok(result);
    }

    [HttpPatch]


    public ActionResult<bool> UpdateOneCategory(CategoryDTO categoryUpdated)
    {
        var newCategory = new Category();
        newCategory.Name = categoryUpdated.Name;
        newCategory.Description = categoryUpdated.Description;
        newCategory.Logo = categoryUpdated.Logo;
        
        var result = _categoryRepository.UpdateSingle(newCategory);
        if (result is false)
        {
            return NotFound($"Category can't be updated!");
        }

        return Ok(result);
    }


    [HttpDelete]

    public void DeleteAll()
    {
        _categoryRepository.DeleteAll();
    }

    [HttpDelete]

    public ActionResult<Category> DeleteSingleCategory(Category categoryToDelete)
    {
        var categories = _categoryRepository.GetAll().ToList();
        var categoryDel = categories.Find(category => category.Id == categoryToDelete.Id);
        if (categoryDel is null)
        {
            return NotFound($"Can't find item to delete!");
        }
         
        var result = _categoryRepository.DeleteSingle(categoryDel).ToList();
        if (result.Count == 0)
        {
            return NotFound($"Can't delete item!");
        }

        return Ok(result);

    }
}