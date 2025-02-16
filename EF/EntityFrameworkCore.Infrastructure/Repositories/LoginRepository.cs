using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Infrastructure.Data;
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
        public LoginRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User?> Login(LoginRequest request)
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

            return userWithRole;
        }

        public async Task<User> Register(User user)
        {
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
