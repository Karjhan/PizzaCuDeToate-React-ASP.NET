using Microsoft.Build.Framework;

namespace PizzaCuDeToateAPI.DTOClasses;

public class CategoryDTO
{
    [Required]
    public string Name { get; set; } 

    public string Description { get; set; } 

    public string Logo { get; set; }
}