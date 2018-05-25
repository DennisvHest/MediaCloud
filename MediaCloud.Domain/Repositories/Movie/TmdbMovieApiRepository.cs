using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Action<Entities.Movie, FileInfo> callback);
    }

    public class TmdbMovieApiRepository : IMovieApiRepository {

        private readonly TMDbClient _tmdbClient;

        public TmdbMovieApiRepository(TMDbClient tmDbClient) {
            _tmdbClient = tmDbClient;
        }

        public async Task<IEnumerable<Entities.Movie>> SearchMovies(IEnumerable<FileInfo> files, Action<Entities.Movie, FileInfo> callback) {
            List<Entities.Movie> foundMovies = new List<Entities.Movie>();

            //Retrieve all movie genres in one request to assign them fully later
            List<Genre> movieGenres = await _tmdbClient.GetMovieGenresAsync();

            foreach (FileInfo file in files) {
                SearchContainer<SearchMovie> searchResult = await _tmdbClient.SearchMovieAsync(Path.GetFileNameWithoutExtension(file.Name), includeAdult: true);
                SearchMovie foundMovie = searchResult.Results.FirstOrDefault();

                if (foundMovie == null) continue;

                Entities.Movie movie = new Entities.Movie(foundMovie);
                movie.ItemGenres = movieGenres
                    .Where(g => foundMovie.GenreIds.Contains(g.Id))
                    .Select(g => new ItemGenre {
                        Item = movie,
                        Genre = new Entities.Genre(g)
                    }).ToList();

                callback(movie, file);

                foundMovies.Add(movie);
            }

            return foundMovies;
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
    }
}
