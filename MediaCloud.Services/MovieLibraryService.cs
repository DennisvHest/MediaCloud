using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using MediaCloud.Domain.Repositories.Movie;

namespace MediaCloud.Services {

    public class MovieLibraryService : ILibraryService<MovieLibrary> {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMovieApiRepository _movieApiRepository;

        public MovieLibraryService(IUnitOfWork unitOfWork, IMovieApiRepository movieApiRepository) {
            _movieApiRepository = movieApiRepository;

            _unitOfWork = unitOfWork;
        }

        public async Task<MovieLibrary> Create(string name, string folderPath, Action<int, string> progressReportCallback) {
            MovieLibrary library = new MovieLibrary { Name = name };

            IEnumerable<Movie> movies = new List<Movie>();
            List<Media> media = new List<Media>();

            //Find video files
            List<string> itemFiles = Directory.GetFileSystemEntries(folderPath, "*.mkv", SearchOption.AllDirectories).ToList();
            itemFiles.AddRange(Directory.GetFileSystemEntries(folderPath, "*.mp4", SearchOption.AllDirectories));

            movies = await _movieApiRepository.SearchMovies(
                itemFiles.Select(f => new FileInfo(f)),
                (movie, file) => {
                    media.Add(new Media { Movie = movie, FileLocation = file.FullName, Library = library });
                }, progressReportCallback);

            library.ItemLibraries = movies.Select(m => new ItemLibrary { Item = m, Library = library }).ToList();
            library.Media = media;

            await _unitOfWork.MovieLibraries.AddOrUpdateInclusive(library);
            await _unitOfWork.Complete();

            return library;
        }

        public async Task<IEnumerable<MovieLibrary>> Get() {
            return await _unitOfWork.MovieLibraries.GetAll();
        }

        public async Task<MovieLibrary> Get(int id) {
            return await _unitOfWork.MovieLibraries.Get(id);
        }
    }
}
