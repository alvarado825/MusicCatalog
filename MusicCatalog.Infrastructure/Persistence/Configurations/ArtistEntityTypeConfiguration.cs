using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Infrastructure.Persistence.Configurations
{
    public class ArtistEntityTypeConfiguration : IEntityTypeConfiguration<Artist>  
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artists");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .HasConversion(
                    v => v.Value,
                    v => new ArtistName(v)
                )
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);
            
            builder.Property(a => a.Biography)
                .HasMaxLength(3000);
            
            builder.Navigation(a => a.Albums)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}