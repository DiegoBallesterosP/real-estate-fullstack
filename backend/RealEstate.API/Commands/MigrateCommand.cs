using RealEstate.Infrastructure.Persistence;

namespace RealEstate.API.Commands
{
    public class MigrateCommand : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrateCommand(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var mongoDbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
            var dataSeeder = new DataSeeder(mongoDbContext.Database); // ✅ Ahora funciona
            
            await dataSeeder.SeedAsync();
            Console.WriteLine("✅ Database migrated and seeded successfully!");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}