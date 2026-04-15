using Microsoft.AspNetCore.Mvc;
using backend.Interface;
using backend.Models.Entities;
using backend.Models.DTOs;
using static backend.Models.DTOs.PermissionDTO;
using static backend.Models.DTOs.RoleDTO;

namespace backend.Controllers
{
    [Route("api/v1/users")]
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
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

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
                    return BadRequest(new { Message = "Registration failed." });

                return StatusCode(201, new { Message = "User registered successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error during registration.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var token = await _userService.LoginAsync(request.Email, request.Password);

                if (token == null)
                    return Unauthorized(new { Message = "Invalid email or password." });

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error during login.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.RoleName))
                    return BadRequest(new { Message = "RoleName is required." });

                var result = await _userService.CreateRoleAsync(request.RoleName);

                if (!result)
                    return BadRequest(new { Message = "Role creation failed." });

                return StatusCode(201, new { Message = "Role created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error creating role.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _userService.AssignRoleAsync(request.Email, request.RoleName);

                if (!result)
                    return BadRequest(new { Message = "Failed to assign role." });

                return Ok(new { Message = "Role assigned successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error assigning role.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("create-permission")]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Name))
                    return BadRequest(new { Message = "Permission name is required." });

                var result = await _userService.CreatePermissionAsync(request.Name, request.Description);

                if (!result)
                    return BadRequest(new { Message = "Failed to create permission." });

                return StatusCode(201, new { Message = "Permission created successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error creating permission.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("assign-permission")]
        public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _userService.AssignPermissionToRoleAsync(request.RoleName, request.PermissionName);

                if (!result)
                    return BadRequest(new { Message = "Failed to assign permission." });

                return Ok(new { Message = "Permission assigned successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error assigning permission.",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("assign-permission-to-user")]
        public async Task<IActionResult> AssignPermissionToUser([FromBody] AssignPermissionToUserRequest request)
        {
            try
            {
                if (request == null)
                    return BadRequest(new { Message = "Invalid request body." });

                var result = await _userService.AssignPermissionToUserAsync(request.Email, request.PermissionName);

                if (!result)
                    return BadRequest(new { Message = "Failed to assign permission to user." });

                return Ok(new { Message = "Permission assigned to user successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error assigning permission to user.",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string? role)
        {
            try
            {
                var users = await _userService.GetUsersAsync(role);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error retrieving users.",
                    Error = ex.Message
                });
            }
        }
    }
}