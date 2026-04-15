using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Infrastructure.Persistence.Configurations
{
    public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(g =>g.Id);

            builder.Property(g => g.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .HasConversion(
                    v => v.Value,
                    v => new GenreName(v)
                )
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);            
        }
    }
}