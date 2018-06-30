using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using Genre = TMDbLib.Objects.General.Genre;

namespace MediaCloud.Domain.Repositories.Movie {

    public interface IMovieApiRepository {

        /// <summary>
        /// Searches the API for the movies with the given query.
        /// </summary>
        /// <param name="query">Title of the movie.</param>
        /// <returns>A list of movies matching the query.</returns>
        Task<IEnumerable<Entities.Movie>> SearchMovie(string query);

        /// <summary>
        /// Retrieves movies matching the given movie files from the API. For every query, the first result is used.
        /// The callback function can be used to add/change something to every found movie (adding media for example).
        /// </summary>
        /// <param name="files">Movie file of the movie to be retrieved</param>
        /// <param name="callback">Callback function executed for every found movie.</param>
        /// <returns>A list of found movies.</returns>
        Task<IEnumerable<Entities.Movie>> SearchMovies(IEnumerable<FileInfo> files,
            Action<Entities.Movie, FileInfo> callback, Action<int, string> progressReportCallback);
    }

    public class TmdbMovieApiRepository : IMovieApiRepository {

        private readonly TMDbClient _tmdbClient;

        private static int _requestCount;
        private static int _movieTaskCount;
        private static double _progress;
        private static Action<int, string> _progressReportCallback;

        public TmdbMovieApiRepository(TMDbClient tmDbClient) {
            _tmdbClient = tmDbClient;
        }

        public async Task<IEnumerable<Entities.Movie>> SearchMovies(IEnumerable<FileInfo> files, Action<Entities.Movie, FileInfo> callback, Action<int, string> progressReportCallback) {
            _progressReportCallback = progressReportCallback;

            //Retrieve all movie genres in one request to assign them fully later
            List<Genre> movieGenres = await _tmdbClient.GetMovieGenresAsync();

            //Create tasks to fetch search results from the API and wait until all of them are complete and add them to the found movies
            _movieTaskCount = files.Count();

            IEnumerable<Task<Entities.Movie>> movieSearchTasks = files.Select(f => SearchMovie(f, movieGenres, callback, progressReportCallback));
            IEnumerable<Entities.Movie> foundMovies = await Task.WhenAll(movieSearchTasks);

            ResetProgress();

            foundMovies = foundMovies.Where(m => m != null);

            return foundMovies;
        }

        private async Task<Entities.Movie> SearchMovie(FileInfo file, IEnumerable<Genre> movieGenres, Action<Entities.Movie, FileInfo> callback, Action<int, string> progressReportCallback) {
            SearchContainer<SearchMovie> searchResult = await _tmdbClient.SearchMovieAsync(Path.GetFileNameWithoutExtension(file.Name), includeAdult: true);
            IncrementRequestCount();
            AddProgress(1, $"Retrieving info for '{Path.GetFileNameWithoutExtension(file.Name)}'");

            SearchMovie foundMovie = searchResult.Results.FirstOrDefault();

            if (foundMovie == null) return null;

            Entities.Movie movie = new Entities.Movie(foundMovie);

            movie.ItemGenres = movieGenres
                .Where(g => foundMovie.GenreIds.Contains(g.Id))
                .Select(g => new ItemGenre {
                    Item = movie,
                    Genre = new Entities.Genre(g)
                }).ToList();

            callback(movie, file);

            return movie;
        }

        public async Task<IEnumerable<Entities.Movie>> SearchMovie(string query) {
            SearchContainer<SearchMovie> searchResult = await _tmdbClient.SearchMovieAsync(query, includeAdult: true);

            //Retrieve all movie genres in one request to assign them fully later
            List<Genre> movieGenres = await _tmdbClient.GetMovieGenresAsync();

            return searchResult.Results.Select(r => {
                Entities.Movie movie = new Entities.Movie(r);

                movie.ItemGenres = movieGenres
                    .Where(g => r.GenreIds.Contains(g.Id))
                    .Select(g => new ItemGenre {
                        Item = movie,
                        Genre = new Entities.Genre(g)
                    }).ToList();

                return movie;
            });
        }

        private static void IncrementRequestCount() {
            _requestCount++;

            if (_requestCount % 30 == 0)
                Thread.Sleep(new TimeSpan(0, 0, 10));
        }

        private static void AddProgress(double singleSeriesProgress, string message = null) {
            _progress += (1 / (double)_movieTaskCount) * singleSeriesProgress;

            _progressReportCallback((int)(_progress * 100), message);
        }

        private static void ResetProgress() {
            _requestCount = 0;
            _movieTaskCount = 0;
            _progress = 0;
            _progressReportCallback = null;
        }
    }
}
