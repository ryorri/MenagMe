using AspNetCore.Identity.Mongo;
using AspNetCore.Identity.Mongo.Model;
using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using MenagMeWebApi.Application.Interfaces.ServiceInterfaces;
using MenagMeWebApi.Domain.Entities;
using MenagMeWebApi.Infrastructure.Data;
using MenagMeWebApi.Infrastructure.Extensions;
using MenagMeWebApi.Infrastructure.Seeders;
using MenagMeWebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace MenagMeWebApi.Infrastructure
{
    public static class InfrastructureInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Scopes

            services.AddSingleton<MenagMeMongoDbContext>();
            services.AddScoped<ServiceExtension>();
            services.AddTransient<IUserSeeder, UserSeeder>();
            services.AddTransient<IRoleSeeder, RoleSeeder>();
            services.AddScoped<DatabaseInitialiser>();

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<ITasksService, TasksService>();


            #endregion


            services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));


            services.AddIdentityMongoDbProvider<User, MongoRole>(identityOptions =>
            {
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = true;
                identityOptions.Password.RequireLowercase = true;

                identityOptions.User.RequireUniqueEmail = true;
            },
            mongoIdentityOptions =>
            {
                var mongoSettings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>();
                mongoIdentityOptions.ConnectionString = $"{mongoSettings!.ConnectionString}/{mongoSettings.DatabaseName}";
            });

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