using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Services {

    public class MovieService : ItemService<Movie>{

        private readonly IMovieApiRepository _movieApiRepository;

        public MovieService(IMovieApiRepository movieApiRepository) {
            _movieApiRepository = movieApiRepository;
        }

        public override async Task<IEnumerable<Movie>> Search(string query) {
            return await _movieApiRepository.SearchMovie(query);
        }
    }
}
