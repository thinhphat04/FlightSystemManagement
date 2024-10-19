using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FlightSystemManagement.Entity;
using FlightSystemManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlightSystemManagement.DTO;
using FlightSystemManagement.Services;
using Microsoft.IdentityModel.Tokens;

namespace FlightSystemManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        // Lấy danh sách tất cả người dùng
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userService.GetUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving users.", details = ex.Message });
            }
        }

        // Lấy thông tin người dùng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user.", details = ex.Message });
            }
        }

        // Tạo người dùng mới RegisterDto
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = new User
                {
                    Email = registerDto.Email,
                    PasswordHash = registerDto.Password,
                    FullName = registerDto.FullName,
                    Role = "User",
                    RefreshToken = _tokenService.CreateRefreshToken(), // Tạo refresh token
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
                };

                var createdUser = await _userService.CreateUserAsync(user);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserID }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the user.", details = ex.Message });
            }
        }

        // Xóa người dùng
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var result = await _userService.DeleteUserAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user.", details = ex.Message });
            }
        }

        // Đăng nhập người dùng
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized("Invalid credentials or incorrect email domain.");

            // Tạo access token
            var accessToken = _tokenService.CreateAccessToken(user);

            // Tạo refresh token
            var refreshToken = _tokenService.CreateRefreshToken();
            await _userService.SaveRefreshTokenAsync(user.UserID, refreshToken);

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            });
        }

        
        // Làm mới Access Token
        // [HttpPost("refresh-token")]
        // public async Task<IActionResult> RefreshToken([FromBody] TokenDto tokenDto)
        // {
        //     var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        //     var userId = int.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //
        //     var savedRefreshToken = await _userService.GetRefreshTokenAsync(userId); // Lấy Refresh Token đã lưu trong hệ thống
        //
        //     if (savedRefreshToken != tokenDto.RefreshToken)
        //     {
        //         return Unauthorized("Invalid refresh token");
        //     }
        //
        //     // Tạo Access Token mới
        //     var user = await _userService.GetUserByIdAsync(userId);
        //     var roles = await _userService.
        //     var newAccessToken = _tokenService.CreateAccessToken(user, roles);
        //     var newRefreshToken = _tokenService.CreateRefreshToken();
        //
        //     // Lưu Refresh Token mới vào hệ thống
        //     await _userService.SaveRefreshTokenAsync(user.UserID, newRefreshToken);
        //
        //     return Ok(new
        //     {
        //         AccessToken = newAccessToken,
        //         RefreshToken = newRefreshToken
        //     });
        // }

    }
}