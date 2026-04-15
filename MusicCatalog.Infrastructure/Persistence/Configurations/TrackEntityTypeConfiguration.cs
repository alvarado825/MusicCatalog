using System.Security.AccessControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicCatalog.Domain.Entities;
using MusicCatalog.Domain.ValueObjects;

namespace MusicCatalog.Infrastructure.Persistence.Configurations
{
    public class TrackEntityTypeConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {          
            builder.ToTable("Tracks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Name)
                .HasConversion(
                    v => v.Value,
                    v => new TrackName(v)
                )
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Composer)
                .HasMaxLength(200);

            builder.Property(t => t.Duration)
                .IsRequired();

            builder.Property(t => t.Bytes)
                .IsRequired();

            builder.Property(t => t.UnitPrice)
                .IsRequired();

            builder.HasIndex(t => t.AlbumId);

            builder.HasIndex(t => t.GenreId);

            builder.HasIndex(t => t.ArtistId);

            builder.HasIndex(new [] { "ArtistId", "Name" })
                .IsUnique()
                .HasDatabaseName("IX_Tracks_Artist_Name");
            
            builder.HasOne<Album>()
                .WithMany(a => a.Tracks)
                .HasForeignKey(t => t.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<Genre>()
                .WithMany()
                .HasForeignKey(t => t.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Artist>()
                .WithMany()
                .HasForeignKey(t => t.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
            }
    }
}