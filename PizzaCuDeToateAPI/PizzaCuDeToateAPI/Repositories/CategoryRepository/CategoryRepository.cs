using Microsoft.EntityFrameworkCore;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Repositories.CategoryRepository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(DbContext context) : base(context)
    {
    }
}