using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Infrastructure.Data;
using EntityFrameworkCore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Infrastructure.RegisterService
{
    public static class AddInfrastructureServices
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString).LogTo(Console.WriteLine));
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ITutionRepository, TutionRepository>();
        }
    }
}
