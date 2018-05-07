using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Repositories.Series {

    public interface ISeriesApiRepository {
        Task<IEnumerable<Entities.Series>> SearchSeries(string query);
    }

    public class TmdbSeriesApiRepository : ISeriesApiRepository {

        private readonly TMDbClient _tmbdClient;

        public TmdbSeriesApiRepository(TMDbClient tmbdClient) {
            _tmbdClient = tmbdClient;
        }

        public async Task<IEnumerable<Entities.Series>> SearchSeries(string query) {
            SearchContainer<SearchTv> searchResult = await _tmbdClient.SearchTvShowAsync(query);

            if (searchResult.Results != null) {
                return searchResult.Results.Select(r => new Entities.Series(r));
            } else {
                return new List<Entities.Series>();
            }
        }
    }
}
