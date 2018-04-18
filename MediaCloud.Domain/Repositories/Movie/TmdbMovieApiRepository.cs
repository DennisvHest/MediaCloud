using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Common;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Repositories.Movie {

    public class TmdbMovieApiRepository : IMovieApiRepository {

        private readonly TMDbClient _tmbdClient;

        public TmdbMovieApiRepository() {
            _tmbdClient = new TMDbClient(Settings.TmdbApiKey);
        }

        public async Task<IEnumerable<Entities.Movie>> SearchMovie(string query) {
            SearchContainer<SearchMovie> searchResult = await _tmbdClient.SearchMovieAsync(query, includeAdult: true);

            return searchResult.Results.Select(r => new Entities.Movie(r));
        }
    }
}
