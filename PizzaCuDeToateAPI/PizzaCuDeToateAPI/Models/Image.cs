using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PizzaCuDeToateAPI.Models;

[Keyless]
public class Image
{
    [Required]
    public string Path { get; set; } = string.Empty;

    public Image()
    {
        
    }
    
    public Image(string path)
    {
        Path = path;
    }
}