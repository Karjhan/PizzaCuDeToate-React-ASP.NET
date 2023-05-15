using System.ComponentModel.DataAnnotations;

namespace PizzaCuDeToateAPI.DTOClasses;

public class LoginUserDTO
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}