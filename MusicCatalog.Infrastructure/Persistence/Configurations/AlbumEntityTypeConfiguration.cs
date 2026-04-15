using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Infrastructure.Persistence.Configurations
{
    public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .HasConversion(
                    v => v.Value,
                    v => new AlbumName(v)
                )
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);
                                    
            builder.HasIndex(a => a.ArtistId);

            builder.HasIndex(new [] { "ArtistId", "Name" })
                   .IsUnique()
                   .HasDatabaseName("IX_Album_Artist_Name");

            builder.HasOne(a => a.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Navigation(a => a.Tracks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}