using EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain.ServiceInterfaces
{
    public interface ILoginService
    {
        public Task<string> Login(LoginRequest request);
        public Task<User> Register(User user);
    }
}
