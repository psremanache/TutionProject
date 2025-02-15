using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Domain.ServiceInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly JwtService _jwtService;
        public LoginService(ILoginRepository loginRepository, JwtService jwtService)
        {
            _loginRepository = loginRepository;
            _jwtService = jwtService;
        }

        public async Task<string> Login(LoginRequest request)
        {
            var user = await _loginRepository.Login(request);
            if(user == null)
            {
                return String.Empty;
            }
            else
            {
                var token = _jwtService.GenerateToken(user.Username, user.Role.RoleName);
                return token;
            }
        }

        public async Task<User> Register(User user)
        {
            return await _loginRepository.Register(user);
        }
    }
}
