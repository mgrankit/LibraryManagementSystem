using LibraryManagementSystem.Application.DTOs.Auth;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepo;
        private readonly ITokenService _tokenService;

        public AuthController(IRepository<User> userRepo, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existing = (await _userRepo.FindAsync(u => u.Username == dto.Username)).FirstOrDefault();
            if (existing != null) return BadRequest("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            return Ok(new { user.Id, user.Username, user.CreatedAt });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var found = (await _userRepo.FindAsync(u => u.Username == dto.Username)).FirstOrDefault();
            if (found == null) return Unauthorized("Invalid credentials");

            bool verified = BCrypt.Net.BCrypt.Verify(dto.Password, found.PasswordHash);
            if (!verified) return Unauthorized("Invalid credentials");

            var token = _tokenService.CreateToken(found.Id, found.Username);
            return Ok(new { token });
        }
    }
}
