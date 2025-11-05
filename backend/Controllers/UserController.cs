using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using backend.Models.Entities;
using backend.Models.DTOs;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ContactNo = request.ContactNo,
                Plant = request.Plant,
                Department = request.Department,
                Designation = request.Designation
            };
            var result = await _userService.RegisterAsync(user, request.Password);
            if (!result)
            {
                return BadRequest("Registration failed.");
            }
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.LoginAsync(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok(new { Token = token });
        }
    }
}
