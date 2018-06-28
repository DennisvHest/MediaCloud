using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaCloud.Common.Models;
using MediaCloud.Domain.Entities;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;
using Genre = TMDbLib.Objects.General.Genre;

namespace MediaCloud.Domain.Repositories.Series {

    public interface ISeriesApiRepository {
        /// <summary>
        /// Searches the API for the series with the given query.
        /// </summary>
        /// <param name="query">Title of the series.</param>
        /// <returns>A list of series matching the query.</returns>
        Task<IEnumerable<Entities.Series>> SearchSeries(string query);

        Task<IEnumerable<Entities.Series>> SearchSeries(
            IEnumerable<IGrouping<string, TvMediaSearchModel>> seriesSearchModels,
            Action<Entities.Series, IGrouping<string, TvMediaSearchModel>> callback);
    }

    public class TmdbSeriesApiRepository : ISeriesApiRepository {

        private readonly TMDbClient _tmdbClient;

        private static int _requestCount;

        public TmdbSeriesApiRepository(TMDbClient tmdbClient) {
            _tmdbClient = tmdbClient;
        }

        public async Task<IEnumerable<Entities.Series>> SearchSeries(string query) {
            SearchContainer<SearchTv> searchResult = await _tmdbClient.SearchTvShowAsync(query);

            if (searchResult.Results != null) {
                return searchResult.Results.Select(r => new Entities.Series(r));
            } else {
                return new List<Entities.Series>();
            }
        }

        public async Task<IEnumerable<Entities.Series>> SearchSeries(IEnumerable<IGrouping<string, TvMediaSearchModel>> seriesSearchModels, Action<Entities.Series, IGrouping<string, TvMediaSearchModel>> callback) {
            //Retrieve all movie genres in one request to assign them fully later
            List<Genre> tvGenres = await _tmdbClient.GetTvGenresAsync();

            //Create tasks to fetch search results from the API and wait until all of them are complete and add them to the found series
            IEnumerable<Task<Entities.Series>> seriesSearchTasks = seriesSearchModels.Select(m => SearchSeries(m, tvGenres, callback));
            IEnumerable<Entities.Series> foundSeries = await Task.WhenAll(seriesSearchTasks);
            _requestCount = 0;
            foundSeries = foundSeries.Where(s => s != null);

            return foundSeries;
        }

        private async Task<Entities.Series> SearchSeries(IGrouping<string, TvMediaSearchModel> seriesSearchModel, IEnumerable<Genre> tvGenres, Action<Entities.Series, IGrouping<string, TvMediaSearchModel>> callback) {
            SearchContainer<SearchTv> searchResult = await _tmdbClient.SearchTvShowAsync(seriesSearchModel.Key);
            IncrementRequestCount();

            IEnumerable<SeasonEpisodePair> seasonEpisodePairs = seriesSearchModel.Select(m => m.SeasonEpisodePair);

            if (searchResult.Results == null) return null;

            SearchTv foundSeries = searchResult.Results[0];
            IEnumerable<int> seasons = seasonEpisodePairs.Select(se => se.Season).Distinct();

            IList<TvSeason> foundSeasons = new List<TvSeason>();

            //Retrieve required seasons
            foreach (int season in seasons) {
                TvSeason foundSeason = await _tmdbClient.GetTvSeasonAsync(foundSeries.Id, season);
                IncrementRequestCount();

                if (foundSeason == null) continue;

                //Remove unwanted episodes
                IEnumerable<int> episodes = seasonEpisodePairs
                    .Where(se => se.Season == season).Select(se => se.Episode);

                foundSeason.Episodes = foundSeason.Episodes.Where(e => episodes.Contains(e.EpisodeNumber)).ToList();

                foundSeasons.Add(foundSeason);
            }

            Entities.Series series = new Entities.Series(foundSeries, foundSeasons);

            series.ItemGenres = tvGenres
                .Where(g => foundSeries.GenreIds.Contains(g.Id))
                .Select(g => new ItemGenre {
                    Item = series,
                    Genre = new Entities.Genre(g)
                }).ToList();

            callback(series, seriesSearchModel);

            return series;
        }

        private async Task<Entities.Series> SearchSingleSeriesInclusive(string query, IEnumerable<SeasonEpisodePair> seasonEpisodePairs) {
            SearchContainer<SearchTv> searchResult = await _tmdbClient.SearchTvShowAsync(query);

            if (searchResult.Results != null) {
                SearchTv foundSeries = searchResult.Results[0];
                IEnumerable<int> seasons = seasonEpisodePairs.Select(se => se.Season).Distinct();

                IList<TvSeason> foundSeasons = new List<TvSeason>();

                //Retrieve required seasons
                foreach (int season in seasons) {
                    TvSeason foundSeason = await _tmdbClient.GetTvSeasonAsync(foundSeries.Id, season);

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

        private static void IncrementRequestCount() {
            _requestCount++;

            if (_requestCount % 30 == 0)
                Thread.Sleep(new TimeSpan(0, 0, 10));
        }
    }
}
