using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Services {

    public class MovieLibraryService : LibraryService<Movie> {

        private readonly IMovieApiRepository _movieApiRepository;

        public MovieLibraryService(IMovieApiRepository movieApiRepository) {
            _movieApiRepository = movieApiRepository;
        }

        public override async Task<Library<Movie>> Create(string name, string folderPath) {
            List<Movie> movies = new List<Movie>();

            string[] itemFiles = Directory.GetFileSystemEntries(folderPath, "*.mkv", SearchOption.AllDirectories);

            foreach (string movieFile in itemFiles) {
                FileInfo file = new FileInfo(movieFile);

                IEnumerable<Movie> foundMovies = await _movieApiRepository.SearchMovie(Path.GetFileNameWithoutExtension(file.Name));

                movies.Add(foundMovies.FirstOrDefault());
            }

            return new Library<Movie> {
                Id = 0,
                Items = movies,
                Name = name
            };
        }
    }
}
