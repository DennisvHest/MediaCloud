using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using MediaCloud.Domain.Repositories.Library;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Services {

    public class MovieLibraryService : ILibraryService<MovieLibrary> {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMovieApiRepository _movieApiRepository;

        public MovieLibraryService(IUnitOfWork unitOfWork, IMovieApiRepository movieApiRepository) {
            _movieApiRepository = movieApiRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<MovieLibrary> Create(string name, string folderPath) {
            List<Movie> movies = new List<Movie>();

            //Find video files
            string[] itemFiles = Directory.GetFileSystemEntries(folderPath, "*.mkv", SearchOption.AllDirectories);

            foreach (string movieFile in itemFiles) {
                FileInfo file = new FileInfo(movieFile);

                //Search movie in API
                IEnumerable<Movie> foundMovies = await _movieApiRepository.SearchMovie(Path.GetFileNameWithoutExtension(file.Name));

                movies.Add(foundMovies.FirstOrDefault());
            }

            MovieLibrary library = new MovieLibrary { Name = name };
            library.ItemLibraries = movies.Select(m => new ItemLibrary {Item = m, Library = library}).ToList();

            await _unitOfWork.MovieLibraries.AddOrUpdateInclusive(library);
            await _unitOfWork.Complete();

            return library;
        }

        public async Task<IEnumerable<MovieLibrary>> Get() {
            return await _unitOfWork.MovieLibraries.GetAllIncludingMovies();
        }

        public async Task<MovieLibrary> Get(int id) {
            return await _unitOfWork.MovieLibraries.GetIncludingMovies(id);
        }
    }
}
