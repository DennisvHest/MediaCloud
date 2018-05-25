﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            List<Entities.Series> foundSeriesAll = new List<Entities.Series>();

            //Retrieve all movie genres in one request to assign them fully later
            List<Genre> tvGenres = await _tmdbClient.GetTvGenresAsync();

            foreach (IGrouping<string, TvMediaSearchModel> seriesSearchModel in seriesSearchModels) {
                SearchContainer<SearchTv> searchResult = await _tmdbClient.SearchTvShowAsync(seriesSearchModel.Key);

                IEnumerable<SeasonEpisodePair> seasonEpisodePairs = seriesSearchModel.Select(m => m.SeasonEpisodePair);

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

                    Entities.Series series = new Entities.Series(foundSeries, foundSeasons);

                    series.ItemGenres = tvGenres
                        .Where(g => foundSeries.GenreIds.Contains(g.Id))
                        .Select(g => new ItemGenre {
                            Item = series,
                            Genre = new Entities.Genre(g)
                        }).ToList();

                    callback(series, seriesSearchModel);

                    foundSeriesAll.Add(series);
                }
            }

            return foundSeriesAll;
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
    }
}
