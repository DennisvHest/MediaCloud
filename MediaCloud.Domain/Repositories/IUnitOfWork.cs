using System;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories.Library;

namespace MediaCloud.Domain.Repositories {

    public interface IUnitOfWork : IDisposable {
        ILibraryRepository Libraries { get; }
        IMovieLibraryRepository MovieLibraries { get; }
        ISeriesLibraryRepository SeriesLibraries { get; }
        IMovieRepository Movies { get; }
        Task<int> Complete();
    }
}