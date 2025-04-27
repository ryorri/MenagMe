using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MenagMeWebApi.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<DatabaseInitialiser>();

            await initialiser.InitializeDB();

            await initialiser.SeedAsync();
        }
    }

    public class DatabaseInitialiser
    {
        private readonly MenagMeDbContext _dBcontext;
        private readonly IRoleSeeder _roleSeeder;
        private readonly IUserSeeder _userSeeder;

        public DatabaseInitialiser(MenagMeDbContext dBcontext, IRoleSeeder roleSeeder, IUserSeeder userSeeder)
        {
            _dBcontext = dBcontext;
            _roleSeeder = roleSeeder;
            _userSeeder = userSeeder;
        }

        public async Task InitializeDB()
        {
            try
            {
                await _dBcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex}");
            }
        }
        public async Task SeedAsync()
        {
            try
            {
                
                await SeedRolesAsync();
                await SeedUsersAsync();
                await _dBcontext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error {ex}");
            }
        }

        public async Task SeedRolesAsync()
        {
            await _roleSeeder.SeedRolesAsync();
        }
        public async Task SeedUsersAsync()
        {
            await _userSeeder.SeedUsersAsync();
        }

    }
}
