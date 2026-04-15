using Microsoft.Extensions.DependencyInjection;
using MusicCatalog.Application.UseCases.Tracks.Commands.CreateTrack;
using FluentValidation;
using MusicCatalog.Application.UseCases.Tracks.Commands.DeactivateTrack;
using MusicCatalog.Application.UseCases.Tracks.Commands.PublishTrack;
using MusicCatalog.Application.UseCases.Tracks.Commands.UpdateTrack;
using MusicCatalog.Application.UseCases.Albums.Command.CreateAlbum;
using MusicCatalog.Application.UseCases.Tracks.Queries.GetPublishedCatalog;
using MusicCatalog.Application.UseCases.Tracks.Queries.GetTrackById;

namespace MusicCatalog.Application
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplicationDependences(this IServiceCollection services)
        {
            //Tracks
            services.AddScoped<CreateTrackUseCase>();        
            services.AddScoped<IValidator<CreateTrackRequest>, CreateTrackRequestValidator>();

            services.AddScoped<DeactivateTrackUseCase>();

            services.AddScoped<PublishTrackUseCase>();

            services.AddScoped<UpdateTrackUseCase>();
            services.AddScoped<IValidator<UpdateTrackRequest>, UpdateTrackValidator>();

            services.AddScoped<GetPublishedCatalogUseCase>();
            services.AddScoped<IValidator<GetPublishedCatalogRequest>, GetPublishedCatalogValidator>();

            services.AddScoped<GetTrackByIdUseCase>();


            //Albums
            services.AddScoped<CreateAlbumUseCase>();
            services.AddScoped<IValidator<CreateAlbumRequest>, CreateAlbumValidator>();

            return services;
        }        
    }
}