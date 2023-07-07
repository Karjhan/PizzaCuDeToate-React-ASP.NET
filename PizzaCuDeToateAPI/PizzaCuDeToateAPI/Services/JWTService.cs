using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Services;

public class JWTService : IJWTService
{
    private const int EXPIRATION_MINUTES = 120;

    private readonly IConfiguration _configuration;
    
    private readonly UserManager<ApplicationUser> _userManager; 

    public JWTService(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    
    public async Task<AuthenticationResponse> CreateToken(ApplicationUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);

        var token = CreateJwtToken(
            await CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponse {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration
        };
    }

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials, DateTime expiration)
    {
        return new JwtSecurityToken(
            _configuration["Jwt:ValidIssuer"],
            _configuration["Jwt:ValidAudience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    }

    private async Task<List<Claim>> CreateClaims(ApplicationUser user)
    {
        var claims = new List<Claim>() {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }
        return claims;
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}