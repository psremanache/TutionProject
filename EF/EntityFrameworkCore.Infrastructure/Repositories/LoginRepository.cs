using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Infrastructure.Data;
using EntityFrameworkCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly DataContext _dataContext;
        private readonly JwtService _jwtService;
        public LoginRepository(DataContext dataContext, JwtService jwtService)
        {
            _dataContext = dataContext;
            _jwtService = jwtService;
        }
        public async Task<string> Login([FromBody] LoginRequest request)
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
                return string.Empty;

            var token = _jwtService.GenerateToken(userWithRole.Username, userWithRole.Role.RoleName);
            return token;
        }

        public async Task<User> Register([FromBody] User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
