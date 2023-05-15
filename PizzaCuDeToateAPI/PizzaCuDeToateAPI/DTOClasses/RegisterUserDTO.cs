using System.ComponentModel.DataAnnotations;

namespace PizzaCuDeToateAPI.DTOClasses;

public class RegisterUserDTO
{
    [Required(ErrorMessage = "Username is required")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }
}