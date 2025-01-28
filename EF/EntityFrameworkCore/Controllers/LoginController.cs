using EntityFrameworkCore.DBConnection;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly JwtService _jwtService;
        public LoginController(DataContext dataContext,JwtService jwtService)
        {
            _dataContext = dataContext;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userWithRole = await _dataContext.Users
            .Where(x => x.Username == request.Username && x.Password == request.Password)
            .Select(user => new User
            {
                Id = user.Id,
                Username = user.Username,
                Role = _dataContext.Roles
                    .Where(role => role.RoleId == user.RoleId)
                    .Select(role => role)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync(); 
            
            if (userWithRole == null)
                        return Unauthorized("Invalid credentials");
            
            var token = _jwtService.GenerateToken(userWithRole.Username, userWithRole.Role.RoleName);
            return Ok(new { Token = token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
