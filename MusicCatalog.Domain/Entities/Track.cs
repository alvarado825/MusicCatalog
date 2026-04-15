using MusicCatalog.Domain.Enums;
using MusicCatalog.Domain.ValueObjects;
using MusicCatalog.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace MusicCatalog.Domain.Entities
{
    public class Track
    {
        public const int ComposerNameMaxLenght = 150;

        public int Id { get; private set; }
        public TrackName Name { get; private set; }
        public int? AlbumId { get; private set; }
        public int? GenreId { get; private set; }
        public int ArtistId {get; private set;}
        public string? Composer { get; private set; }
        public TimeSpan Duration { get; private set; }
        public int Bytes { get; private set; }
        public Decimal UnitPrice { get; private set; }
        public TrackStatusEnum TrackStatus {get; private set;}
        public bool IsActive {get; private set;}

        public Track(TrackName name, int? albumId, int? genreId, int artistId, string? composer, TimeSpan duration, int bytes, decimal unitPrice, TrackStatusEnum trackStatus, bool isActive)
        {
            //Campos obrigatorios
            ChangeName(name);           
            SetArtistId(artistId);
            SetDuration(duration);
            SetBytes(bytes);
            ChangeUnitPrice(unitPrice);
            TrackStatus = trackStatus;
            IsActive = isActive;

            //Campos opcionais
            ChangeAlbumId(albumId);
            ChangeGenreId(genreId);
            ChangeComposer(composer);

        }

        //Construtor para EF core
        private Track (){}

        public void Publish()
        {
            if(!IsActive)
                throw new DomainRuleViolationException(ErrorCode.TrackInactive, "Unable to publish: the track must be Active.");
            
            if(AlbumId is null || AlbumId <= 0)
                throw new DomainRuleViolationException(ErrorCode.AlbumIdIsNullOrEmpty, "Unable to publish: AlbumID is null or empty.");

            if(GenreId is null || GenreId <= 0)
                throw new DomainRuleViolationException(ErrorCode.GenreIdIsNullOrEmpty, "Unable to publish: GenreId is null or empty.");


            if(TrackStatus == TrackStatusEnum.Draft)
                TrackStatus = TrackStatusEnum.Published;
            else 
                 throw new DomainRuleViolationException(ErrorCode.TrackIsAlreadyPublished, "Track is already published.");  
        }

        public void Deactivate()
        {
            if(!IsActive)
                throw new DomainRuleViolationException(ErrorCode.TrackIsAlreadyInactive,"Track is already Inactive.");

            IsActive = false;
        }

        #region Setters
        //Set como private, pois define um valor obrigatorio e invariante.
        //Change public para ser acessado fora da entidade, geralmente para campos que o update é permitido
        private void SetArtistId(int artistId)
        {
            if (artistId <= 0)
                throw new DomainException("ArtistId must be greater than 0.");

            ArtistId = artistId;
        }

        private void SetDuration(TimeSpan duration)
        {
            if (duration <= TimeSpan.Zero)
                throw new DomainException("Duration must be greather than 00:00.");

            Duration = duration;
        }

        private void SetBytes(int bytes)
        {
            if (bytes <= 0)
                throw new DomainException("Bytes must be greater than 0.");

            Bytes = bytes;
        }

        public void ChangeUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new DomainException("UnitPrice must be positive.");

            UnitPrice = unitPrice;
        }

         public void ChangeName(TrackName name)
        {
            if (name is null)
                throw new DomainException("Name is required.");

            Name = name;
        }


        public void ChangeAlbumId(int? albumId)
        {
            if (albumId is not null && albumId <= 0)
                throw new DomainException("albumId must be greater than 0.");

            AlbumId = albumId;
        }

        public void ChangeGenreId(int? genreId)
        {
            if (genreId is not null && genreId <= 0)
                throw new DomainException("GenreId must be greater than 0.");

            GenreId = genreId;
        }


        public void ChangeComposer(string? composer)
        {
            string? trimmed = composer?.Trim();

            if(string.IsNullOrWhiteSpace(trimmed))
            {
                Composer = null;
            }
            else
            {        
                var singleSpaced = Regex.Replace(trimmed, @"\s+", " ");

                if (singleSpaced.Length > ComposerNameMaxLenght)
                    throw new DomainException($"composer must be {ComposerNameMaxLenght} characters lenght.");

                Composer = singleSpaced;
            }           
        }

        #endregion                      
    }
}