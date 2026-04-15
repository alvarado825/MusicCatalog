using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicCatalog.Application.Interfaces.Persistence;
using MusicCatalog.Infrastructure.Persistence;
using MusicCatalog.Infrastructure.Persistence.Repositories;
using MusicCatalog.Infrastructure.Persistence.UnitOfWork;

namespace MusicCatalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependences(this IServiceCollection services, IConfiguration configuration)
        {
            
            string mySqlConnection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MusicCatalogDbContext>(options =>
                                options.UseMySql(mySqlConnection,
                                ServerVersion.AutoDetect(mySqlConnection)));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            
            services.AddScoped<IAlbumRepository, AlbumRepository>();

            services.AddScoped<IArtistRepository, ArtistRepository>();

            services.AddScoped<IGenreRepository, GenreRepository>();

            services.AddScoped<ITrackRepository, TrackRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        
    }
}