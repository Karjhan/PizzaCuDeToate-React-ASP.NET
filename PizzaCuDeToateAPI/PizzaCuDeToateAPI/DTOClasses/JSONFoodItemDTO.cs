using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.DTOClasses;

public class JSONFoodItemDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public double UnitPrice { get; set; }

    public List<int> IngredientIds { get; set; } = new List<int>();

    public List<string> Images { get; set; } = new List<string>();

    public string Logo { get; set; }

    public void GetFromFoodItem(FoodItem origin)
    {
        Id = origin.Id;
        Name = origin.Name;
        Description = origin.Description;
        UnitPrice = origin.UnitPrice;
        Logo = origin.Logo;
        Images = origin.Images;
        CategoryId = origin.Category.Id;
        IngredientIds = origin.Ingredients.Select(ingredient => ingredient.Id).ToList();
    }
}