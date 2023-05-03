using System.ComponentModel.DataAnnotations;

namespace PizzaCuDeToateAPI.DTOClasses;

public class CategoryAddDTO
{
    [Required]
    public string Name { get; set; } 

    public string Description { get; set; } 

    public string Logo { get; set; }
}