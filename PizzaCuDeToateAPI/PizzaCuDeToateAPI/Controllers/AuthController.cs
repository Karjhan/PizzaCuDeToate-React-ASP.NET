using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register/{role}")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO request, [FromRoute] string role)
        {
            var findUser = await _userManager.FindByEmailAsync(request.Email);
            if (findUser is not null)
            {
                return BadRequest("User already exists!");
            }
            IdentityUser newUser = new IdentityUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.Username
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(500, "User failed to create!");
                }
                await _userManager.AddToRoleAsync(newUser, role);
                return Ok("User created successfully!");
            }
            return StatusCode(500, "This role doesn't exist!");
        }
        
        [HttpGet("username={username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            var findUser = await _userManager.FindByNameAsync(username);
            if (findUser is null)
            {
                return NotFound();
            }
            return Ok(new string[]
            {
                findUser.UserName,
                findUser.Email
            });
        }
        
        [HttpGet("email={email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return NotFound();
            }
            return Ok(new string[]
            {
                findUser.UserName,
                findUser.Email
            });
        }
    }
}
