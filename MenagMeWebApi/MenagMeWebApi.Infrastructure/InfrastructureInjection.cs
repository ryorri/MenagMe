using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Data;
using MenagMeWebApi.Infrastructure.Extensions;
using MenagMeWebApi.Infrastructure.Seeders;
using MenagMeWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;

namespace MenagMeWebApi.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Scopes
            services.AddScoped<DatabaseInitialiser>();
            services.AddScoped<ServiceExtension>();
            services.AddScoped<IRoleSeeder, RoleSeeder>();
            services.AddScoped<IUserSeeder, UserSeeder>();


            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            

            #endregion


            services.AddDbContext<MenagMeDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<MenagMeDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserOnly", policy => policy.RequireClaim(ClaimTypes.NameIdentifier));
            });

            services.AddControllers();

        }
    }
}