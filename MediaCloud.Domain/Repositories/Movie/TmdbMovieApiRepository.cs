using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Repositories.Movie {

    public interface IMovieApiRepository {

        /// <summary>
        /// Searches the API for the movies with the given query.
        /// </summary>
        /// <param name="query">Title of the movie.</param>
        /// <returns>A list of movies matching the query.</returns>
        Task<IEnumerable<Entities.Movie>> SearchMovie(string query);
    }

    public class TmdbMovieApiRepository : IMovieApiRepository {

        private readonly TMDbClient _tmbdClient;

        public TmdbMovieApiRepository(TMDbClient tmDbClient) {
            _tmbdClient = tmDbClient;
        }

        public async Task<IEnumerable<Entities.Movie>> SearchMovie(string query) {
            SearchContainer<SearchMovie> searchResult = await _tmbdClient.SearchMovieAsync(query, includeAdult: true);

            return searchResult.Results.Select(r => new Entities.Movie(r));
        }
    }
}
