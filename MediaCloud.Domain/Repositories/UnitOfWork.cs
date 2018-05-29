using System.Threading.Tasks;
using MediaCloud.Domain.Repositories.Episode;
using MediaCloud.Domain.Repositories.Item;
using MediaCloud.Domain.Repositories.Library;
using MediaCloud.Domain.Repositories.Media;
using MediaCloud.Domain.Repositories.Movie;
using MediaCloud.Domain.Repositories.Season;

namespace MediaCloud.Domain.Repositories {

    public class UnitOfWork : IUnitOfWork {

        private readonly MediaCloudContext _context;

        public ILibraryRepository Libraries { get; }
        public IMovieLibraryRepository MovieLibraries { get; }
        public ISeriesLibraryRepository SeriesLibraries { get; }
        public IItemRepository Items { get; }
        public IMovieRepository Movies { get; }
        public ISeasonRepository Seasons { get; }
        public IEpisodeRepository Episodes { get; }
        public IMediaRepository Media { get; }

        public UnitOfWork(MediaCloudContext context) {
            _context = context;
            MovieLibraries = new MovieLibraryRepository(context);
            SeriesLibraries = new SeriesLibraryRepository(context);
            Libraries = new LibraryRepository(context);
            Items = new ItemRepository(context);
            Movies = new MovieRepository(context);
            Seasons = new SeasonRepository(context);
            Episodes = new EpisodeRepository(context);
            Media = new MediaRepository(context);
        }

        public async Task<int> Complete() {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}