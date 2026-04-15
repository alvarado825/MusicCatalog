using Microsoft.EntityFrameworkCore;
using MusicCatalog.Domain.Entities;

namespace MusicCatalog.Infrastructure.Persistence
{
    public class MusicCatalogDbContext : DbContext
    {
        public MusicCatalogDbContext(DbContextOptions<MusicCatalogDbContext> options) : base(options)
        {}

        public DbSet<Album> Albuns {get;set;}
        public DbSet<Artist> Artists {get;set;}
        public DbSet<Genre> Genres {get;set;}
        public DbSet<Track> Tracks {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MusicCatalogDbContext).Assembly);
        }
    }
}