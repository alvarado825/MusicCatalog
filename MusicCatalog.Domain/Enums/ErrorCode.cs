namespace MusicCatalog.Domain.Enums
{
    public enum ErrorCode
    {
        Unknown = 0,
        TrackNotFound = 1,
        TrackInactive = 2,
        TrackIsAlreadyPublished = 3,
        TrackIsAlreadyInactive = 4,
        TrackAlreadyExistToThisArtist = 5,
        AlbumIdIsNullOrEmpty = 6,
        GenreIdIsNullOrEmpty = 7,
    }
}