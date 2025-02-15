using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Domain.ServiceInterfaces;
using EntityFrameworkCore.Entities;
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
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public Task<string> Login(LoginRequest request)
        {
            return _loginRepository.Login(request);
        }

        public Task<User> Register(User user)
        {
            return _loginRepository.Register(user);
        }
    }
}
