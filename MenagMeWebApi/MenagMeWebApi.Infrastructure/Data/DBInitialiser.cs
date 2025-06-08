using MenagMeWebApi.Application.Interfaces.ISeederInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MenagMeWebApi.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<DatabaseInitialiser>();

            await initialiser.SeedAsync();
        }
    }

    public class DatabaseInitialiser
    {
        private readonly MenagMeMongoDbContext _dBcontext;
        private readonly IRoleSeeder _roleSeeder;
        private readonly IUserSeeder _userSeeder;

        public DatabaseInitialiser(MenagMeMongoDbContext dBcontext, IRoleSeeder roleSeeder, IUserSeeder userSeeder)
        {
            _dBcontext = dBcontext;
            _roleSeeder = roleSeeder;
            _userSeeder = userSeeder;
        }
        public async Task SeedAsync()
        {
            try
            {

                await SeedRolesAsync();
                await SeedUsersAsync();

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