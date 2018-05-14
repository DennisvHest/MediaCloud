using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Common.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MediaCloud.Domain.Repositories.Series {

    public interface ISeriesApiRepository {
        Task<IEnumerable<Entities.Series>> SearchSeries(string query);
        Task<Entities.Series> SearchSingleSeriesInclusive(string query,
            IEnumerable<SeasonEpisodePair> seasonEpisodePairs);
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

        public async Task<Entities.Series> SearchSingleSeriesInclusive(string query, IEnumerable<SeasonEpisodePair> seasonEpisodePairs) {
            SearchContainer<SearchTv> searchResult = await _tmbdClient.SearchTvShowAsync(query);

            if (searchResult.Results != null) {
                SearchTv foundSeries = searchResult.Results[0];
                IEnumerable<int> seasons = seasonEpisodePairs.Select(se => se.Season).Distinct();

                IList<TvSeason> foundSeasons = new List<TvSeason>();

                //Retrieve required seasons
                foreach (int season in seasons) {
                    TvSeason foundSeason = await _tmbdClient.GetTvSeasonAsync(foundSeries.Id, season);
                    
                    if (foundSeason == null) continue;

                    //Remove unwanted episodes
                    IEnumerable<int> episodes = seasonEpisodePairs
                        .Where(se => se.Season == season).Select(se => se.Episode);

                    foundSeason.Episodes = foundSeason.Episodes.Where(e => episodes.Contains(e.EpisodeNumber)).ToList();

                    foundSeasons.Add(foundSeason);
                }

                return new Entities.Series(foundSeries, foundSeasons);
            } else {
                return null;
            }
        }
    }
}
