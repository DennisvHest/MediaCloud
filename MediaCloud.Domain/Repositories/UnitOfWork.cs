using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories.Library;

namespace MediaCloud.Domain.Repositories {

    public class UnitOfWork : IUnitOfWork {

        private readonly MediaCloudContext _context;

        public ILibraryRepository Libraries { get; }
        public IMovieLibraryRepository MovieLibraries { get; }
        public ISeriesLibraryRepository SeriesLibraries { get; }
        public IMovieRepository Movies { get; }

        public UnitOfWork(MediaCloudContext context) {
            _context = context;
            Libraries = new LibraryRepository(context);
            MovieLibraries = new MovieLibraryRepository(context);
            SeriesLibraries = new SeriesLibraryRepository(context);
            Movies = new MovieRepository(context);
        }

        public async Task<int> Complete() {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}