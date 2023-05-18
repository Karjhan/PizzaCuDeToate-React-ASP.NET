using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PizzaCuDeToateAPI.DTOClasses;
using PizzaCuDeToateAPI.Models;
using PizzaCuDeToateAPI.Services;

namespace PizzaCuDeToateAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IJWTService _jwtService;
        private readonly IGoogleService _googleService;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, 
            IEmailService emailService, 
            IJWTService jwtService,
            IGoogleService googleService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
            _jwtService = jwtService;
            _googleService = googleService;
        }

        [HttpGet("register/google/{tokenId}")]
        public async Task<IActionResult> GoogleRegister([FromRoute]string tokenId)
        {
            try
            {
                // var payload = await GoogleJsonWebSignature.ValidateAsync(tokenId, new GoogleJsonWebSignature.ValidationSettings());
                var payload = await _googleService.GetUserData(tokenId);
                var findUser = await _userManager.FindByEmailAsync(payload.email);
                if (findUser is not null)
                {
                    var response = new
                    {
                        error = $"User with email {payload.email} already exists!"
                    };
                    return BadRequest(JsonConvert.SerializeObject(response));
                }
                ApplicationUser newUser = new ApplicationUser()
                {
                    Email = payload.email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = Regex.Replace(payload.name, "[^a-zA-Z0-9]", String.Empty).Replace(" ", "")
                };
                var result = await _userManager.CreateAsync(newUser);
                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors);
                }
                await _userManager.AddToRoleAsync(newUser, "user");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new {token, email = newUser.Email}, Request.Scheme);
                var message = new MailMessage(new[] { newUser.Email }, "Confirmation email link",
                    $"Thank you for choosing our food services. Please click on the following link to confirm your account:\nConfirmation link: {confirmationLink!}");
                _emailService.SendEmail(message);
                var responseJson = new
                {
                    success = $"User created successfully and confirmation email sent to {newUser.Email}!",
                    email = newUser.Email
                };
                return Ok(JsonConvert.SerializeObject(responseJson));
            }
            catch (Exception e)
            {
                var response = new
                {
                    error = "Something went wrong with Google authentication!"
                };
                return BadRequest(JsonConvert.SerializeObject(response));
            }
        }

        [HttpPost("register/{role}")]
        public async Task<IActionResult> NormalRegister([FromBody] RegisterUserDTO request, [FromRoute] string role)
        {
            var findUser = await _userManager.FindByEmailAsync(request.Email);
            if (findUser is not null)
            {
                var response = new
                {
                    error = $"User with email {request.Email} already exists!"
                };
                return BadRequest(JsonConvert.SerializeObject(response));
            }
            ApplicationUser newUser = new ApplicationUser()
            {
                Email = request.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                PhoneNumber = request.PhoneNumber.Length > 0 ? request.PhoneNumber : null,
                Address = request.Address.Length > 0 ? request.Address : null,
                UserName = request.Username
            };
            if (await _roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(500, result.Errors);
                }
                await _userManager.AddToRoleAsync(newUser, role);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new {token, email = newUser.Email}, Request.Scheme);
                var message = new MailMessage(new[] { newUser.Email }, "Confirmation email link",
                    $"Thank you for choosing our food services. Please click on the following link to confirm your account:\nConfirmation link: {confirmationLink!}");
                _emailService.SendEmail(message);
                var response = new
                {
                    success = $"User created successfully and confirmation email sent to {newUser.Email}!"
                };
                return Ok(JsonConvert.SerializeObject(response));
            }
            var responseJson = new
            {
                error = "This role doesn't exist!"
            };
            return StatusCode(500, JsonConvert.SerializeObject(responseJson));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO request)
        {
            var findUser = await _userManager.FindByEmailAsync(request.Email);
            if (findUser is not null && await _userManager.CheckPasswordAsync(findUser, request.Password))
            {
                var response = await _jwtService.CreateToken(findUser);
                return Ok(response);
            }
            var responseJson = new
            {
                error = "Invalid credentials!"
            };
            return Unauthorized(JsonConvert.SerializeObject(responseJson));
        }
        
        [HttpGet("username={username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            var findUser = await _userManager.FindByNameAsync(username);
            if (findUser is null)
            {
                return NotFound();
            }
            var responseJson = new
            {
                username = findUser.UserName,
                email = findUser.Email,
                address = findUser.Address,
                phoneNumber = findUser.PhoneNumber,
                logo = findUser.Logo
            };
            return Ok(JsonConvert.SerializeObject(responseJson));
        }
        
        [HttpGet("email={email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return NotFound();
            }
            var responseJson = new
            {
                username = findUser.UserName,
                email = findUser.Email,
                address = findUser.Address,
                phoneNumber = findUser.PhoneNumber,
                logo = findUser.Logo
            };
            return Ok(JsonConvert.SerializeObject(responseJson));
        }

        [HttpPut("email={email}")]
        public async Task<IActionResult> AddProfileImage([FromRoute] string email, [FromBody] UpdateUserDetailsDTO request)
        {
            var findUser = await _userManager.FindByEmailAsync(email);
            if (findUser is null)
            {
                return NotFound();
            }
            if (request.Username.Length > 0)
            {
                findUser.UserName = request.Username;
            }
            if (request.Address.Length > 0)
            {
                findUser.Address = request.Address;
            }
            if (request.PhoneNumber.Length > 0)
            {
                findUser.PhoneNumber = request.PhoneNumber;
            }
            if (request.LogoPath.Length > 0)
            {
                findUser.Logo = request.LogoPath;
            }
            await _userManager.UpdateAsync(findUser);
            var responseJson = new
            {
                username = findUser.UserName,
                email = findUser.Email,
                address = findUser.Address,
                phoneNumber = findUser.PhoneNumber,
                logo = findUser.Logo
            };
            return Ok(JsonConvert.SerializeObject(responseJson));
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
                    var responseJson = new
                    {
                        success = "Email verified successfully!"
                    };
                    return Ok(JsonConvert.SerializeObject(responseJson));
                }
            }
            var response = new
            {
                error = "This user doesn't exist!"
            };
            return BadRequest(JsonConvert.SerializeObject(response));
        }
    }
}
