using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace PizzaCuDeToateAPI.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public virtual string? Address { get; set; } 

    public ApplicationUser()
    {
        
    }
}