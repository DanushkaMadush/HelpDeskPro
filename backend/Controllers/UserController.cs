using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using backend.Models.Entities;
using backend.Models.DTOs;
using static backend.Models.DTOs.PermissionDTO;
using static backend.Models.DTOs.RoleDTO;

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

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var result = await _userService.CreateRoleAsync(request.RoleName);
            if (!result) return BadRequest("Role creation failed.");

            return Ok(new { message = "Role created successfully." });
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            var result = await _userService.AssignRoleAsync(request.Email, request.RoleName);
            if (!result) return BadRequest("Failed to assign role.");

            return Ok(new { message = "Role assigned successfully." });
        }

        [HttpPost("create-permission")]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
        {
            var result = await _userService.CreatePermissionAsync(request.Name, request.Description);
            if (!result) return BadRequest("Failed to create permission.");
            return Ok(new { message = "Permission created successfully." });
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionRequest request)
        {
            var result = await _userService.AssignPermissionToRoleAsync(request.RoleName, request.PermissionName);
            if (!result) return BadRequest("Failed to assign permission.");
            return Ok(new { message = "Permission assigned successfully." });
        }

        [HttpPost("assign-permission-to-user")]
        public async Task<IActionResult> AssignPermissionToUser([FromBody] AssignPermissionToUserRequest request)
        {
            var result = await _userService.AssignPermissionToUserAsync(request.Email, request.PermissionName);
            if (!result) return BadRequest("Failed to assign permission to user.");

            return Ok(new { message = "Permission assigned to user successfully." });
        }




    }
}
