using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Domain.Repositories {

    public class MovieRepository {

        private readonly IMovieApiRepository _movieApiRepository;

        public MovieRepository(IMovieApiRepository movieApiRepository) {
            _movieApiRepository = movieApiRepository;
        }
    }
}
