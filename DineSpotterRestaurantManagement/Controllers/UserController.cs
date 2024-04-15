using DineSpotterRestaurantManagement.Helpers;
using DineSpotterRestaurantManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestaurantReservation.Services.Implementation;
using RestaurantReservation.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public record RegisterRequest(string firstName, string lastName, string username, string email, string password, string phoneNumber)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var existingUser = await _userService.GetUserByUsernameAsync(request.username);
                if (existingUser != null)
                {
                    return BadRequest( new { Message = "Username already exists."});
                }

                existingUser = await _userService.GetUserByEmailAsync(request.email);
                if (existingUser != null)
                {
                    return BadRequest( new { Message = "Email already exists." });
                }

                existingUser = await _userService.GetUserByPhoneNumberAsync(request.phoneNumber);
                if (existingUser != null)
                {
                    return BadRequest( new { Message = "Phone number already exists." });
                }

                if (!await _userService.CheckPasswordStrength(request.password))
                {
                    throw new ApplicationException("Password does not meet the requirements.");
                }

                var hashedPassword = await _userService.HashPasswordAsync(request.password);

                var registeredUser = await _userService.RegisterUserAsync(request.firstName, request.lastName, request.username, request.email, hashedPassword, request.phoneNumber);
                return Ok( new { Message = "Thank you for registering with DineSpotter! Your account has been successfully created. Start exploring and booking your favorite restaurants now!" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public record LoginRequest(string username, string password)
        {
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(login.username);
                if (user == null)
                {
                    throw new ApplicationException("Incorrect username or it does not exist.");
                }

                var isPasswordCorrect = await _userService.VerifyPasswordAsync(login.password, user.Password);
                if (!isPasswordCorrect)
                {
                    throw new ApplicationException("Incorrect password.");
                }

                var token = GenerateJwtToken(user);
                return Ok(new { token, Message = "Login Successful!" });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("DineSpotterSecret123456789101112");
            var identity = new ClaimsIdentity(new Claim[]
            {
                    new Claim("Username", user.Username),
                    new Claim("Id", user.Id.ToString())
                });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        public record UpdateUserRequest(int id, string name, string lastName, string username, string email, string password, string phoneNumber)
        {
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest update)
        {
            try
            {
                var result = await _userService.UpdateUserAsync(update.id, update.name, update.lastName, update.username, update.email, update.password, update.phoneNumber);
                if (result)
                {
                    return Ok("User details updated.");
                }
                return NotFound();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result)
            {
                return Ok("User deleted.");
            }
            return NotFound();
        }
    }
}
