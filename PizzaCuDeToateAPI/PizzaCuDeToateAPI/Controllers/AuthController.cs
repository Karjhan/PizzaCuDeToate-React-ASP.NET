using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Services;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IJWTService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,IConfiguration configuration, IEmailService emailService, IJWTService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _jwtService = jwtService;
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
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new {token, email = newUser.Email}, Request.Scheme);
                var message = new MailMessage(new[] { newUser.Email }, "Confirmation email link",
                    $"Thank you for choosing our food services. Please click on the following link to confirm your account:\n{confirmationLink!}");
                _emailService.SendEmail(message);
                return Ok($"User created successfully and confirmation email sent to {newUser.Email}!");
            }
            return StatusCode(500, "This role doesn't exist!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Register([FromBody] LoginUserDTO request)
        {
            var findUser = await _userManager.FindByEmailAsync(request.Email);
            if (findUser is not null && await _userManager.CheckPasswordAsync(findUser, request.Password))
            {
                var response = await _jwtService.CreateToken(findUser);
                return Ok(response);
            }
            return Unauthorized("Invalid credentials!");
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

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return Ok("Email verified successfully!");
                }
            }
            return BadRequest("This user doesn't exist!");
        }
    }
}
