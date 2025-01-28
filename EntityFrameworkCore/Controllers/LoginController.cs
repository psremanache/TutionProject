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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);
            if (user == null) 
                return Unauthorized("Invalid credentials");

            var token = _jwtService.GenerateToken(user.Username, user.Role.RoleName);
            return Ok(new { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok(user);
        }
    }
}
