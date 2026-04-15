using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MusicCatalog.Infrastructure.Persistence.Design
{
    public class MusicCatalogDbContextFactory : IDesignTimeDbContextFactory<MusicCatalogDbContext>
    {
        public MusicCatalogDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MusicCatalog.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<MusicCatalogDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new MusicCatalogDbContext(optionsBuilder.Options);
        }
    }
}