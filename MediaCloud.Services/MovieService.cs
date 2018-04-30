using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using MediaCloud.Domain.Repositories.Library;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Services {

    public class MovieService : ItemService<Movie> {

        private readonly IMovieApiRepository _movieApiRepository;

        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IMovieApiRepository movieApiRepository, IUnitOfWork unitOfWork) {
            _movieApiRepository = movieApiRepository;

            _unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Movie>> Search(string query) {
            return await _movieApiRepository.SearchMovie(query);
        }
        public override async Task<Movie> Add(Movie movie) {
            await _unitOfWork.Movies.Add(movie);
            await _unitOfWork.Complete();

            return movie;
        }
    }
}
