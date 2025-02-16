using EntityFrameworkCore.Application.Services;
using EntityFrameworkCore.Domain.RepositoryInterfaces;
using EntityFrameworkCore.Domain.ServiceInterfaces;
using EntityFrameworkCore.Entities;
using EntityFrameworkCore.Infrastructure.Data;
using EntityFrameworkCore.Infrastructure.Repositories;
using EntityFrameworkCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Application.RegisterService
{
    public static class AddApplicationService
    {
        public static void AddApplication(this IServiceCollection services, IConfigurationSection jwtSetting)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = jwtSetting;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Log the error message here
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };

            });

            services.Configure<JwtSettings>(jwtSetting);

            // Register JwtService
            services.AddSingleton<JwtService>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Student", policy => policy.RequireRole("Student"));
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Instructor", policy => policy.RequireRole("Instructor"));
                options.AddPolicy("AllPolicy", policy => policy.RequireRole("Student", "Instructor", "Admin"));
            });
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ITutionService, TutionService>();
        }
    }
}
