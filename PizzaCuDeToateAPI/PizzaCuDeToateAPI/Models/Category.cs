namespace PizzaCuDeToateAPI.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Logo { get; set; }

    public Category()
    {
        
    }

    public Category(string name, string description, string logo)
    {
        Name = name;
        Description = description;
        Logo = logo;
    }
}