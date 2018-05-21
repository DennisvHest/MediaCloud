using System;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories.Item;
using MediaCloud.Domain.Repositories.Library;
using MediaCloud.Domain.Repositories.Media;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Domain.Repositories {

    public interface IUnitOfWork : IDisposable {
        ILibraryRepository Libraries { get; }
        IMovieLibraryRepository MovieLibraries { get; }
        ISeriesLibraryRepository SeriesLibraries { get; }
        IItemRepository Items { get; }
        IMovieRepository Movies { get; }
        IMediaRepository Media { get; }
        Task<int> Complete();
    }
}