using System;
using System.Threading.Tasks;
using MediaCloud.Domain.Repositories.Episode;
using MediaCloud.Domain.Repositories.Genre;
using MediaCloud.Domain.Repositories.Item;
using MediaCloud.Domain.Repositories.Library;
using MediaCloud.Domain.Repositories.Media;
using MediaCloud.Domain.Repositories.Movie;
using MediaCloud.Domain.Repositories.Season;

namespace MediaCloud.Domain.Repositories {

    public interface IUnitOfWork : IDisposable {
        ILibraryRepository Libraries { get; }
        IMovieLibraryRepository MovieLibraries { get; }
        ISeriesLibraryRepository SeriesLibraries { get; }
        IItemRepository Items { get; }
        IGenreRepository Genres { get; }
        IMovieRepository Movies { get; }
        ISeasonRepository Seasons { get; }
        IEpisodeRepository Episodes { get; }
        IMediaRepository Media { get; }
        Task<int> Complete();
    }
}