using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Services;

public interface IJWTService
{
    Task<AuthenticationResponse> CreateToken(ApplicationUser user);
}