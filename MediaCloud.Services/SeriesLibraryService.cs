using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaCloud.Common.Models;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using MediaCloud.Domain.Repositories.Series;
using MoreLinq;

namespace MediaCloud.Services {

    public class SeriesLibraryService : ILibraryService<SeriesLibrary> {

        private readonly IUnitOfWork _unitOfWork;

        private readonly ISeriesApiRepository _seriesApiRepository;

        public SeriesLibraryService(IUnitOfWork unitOfWork, ISeriesApiRepository seriesApiRepository) {
            _unitOfWork = unitOfWork;
            _seriesApiRepository = seriesApiRepository;
        }

        public async Task<SeriesLibrary> Create(string name, string folderPath, Action<int, string> progressReportCallback) {
            IEnumerable<Series> series = new List<Series>();
            List<Media> media = new List<Media>();

            SeriesLibrary library = new SeriesLibrary { Name = name };

            //Find video files
            List<string> itemFiles = Directory.GetFileSystemEntries(folderPath, "*.mkv", SearchOption.AllDirectories).ToList();
            itemFiles.AddRange(Directory.GetFileSystemEntries(folderPath, "*.mp4", SearchOption.AllDirectories));

            //Group series by title
            Regex regex = new Regex(@"(.+)\s+?(S[0-9]{1,2}E[0-9]{1,2})");

            IList<TvMediaSearchModel> searchModels = new List<TvMediaSearchModel>();

            foreach (string itemFile in itemFiles) {
                FileInfo fileInfo = new FileInfo(itemFile);
                Match match = regex.Match(Path.GetFileNameWithoutExtension(fileInfo.Name));

                if (match.Success) {
                    searchModels.Add(new TvMediaSearchModel {
                        Title = match.Groups[1].Value,
                        FileInfo = fileInfo,
                        SeasonEpisodePair = new SeasonEpisodePair(match.Groups[2].Value)
                    });
                }
            }

            IEnumerable<IGrouping<string, TvMediaSearchModel>> groupedSeries = searchModels.GroupBy(m => m.Title);

            series = await _seriesApiRepository.SearchSeries(groupedSeries, (foundSeries, seriesSearchModel) => {
                foreach (Season season in foundSeries.Seasons) {
                    foreach (Episode episode in season.Episodes) {
                        TvMediaSearchModel episodeFile = seriesSearchModel.FirstOrDefault(m =>
                            m.SeasonEpisodePair.Season == season.SeasonNumber &&
                            m.SeasonEpisodePair.Episode == episode.EpisodeNumber);

                        if (episodeFile == null) continue;

                        media.Add(new Media { Episode = episode, FileLocation = episodeFile.FileInfo.FullName, Library = library });
                    }
                }
            }, progressReportCallback);

            series = series.DistinctBy(s => s.Id).ToList();
            
            library.ItemLibraries = series.Select(m => new ItemLibrary { Item = m, Library = library }).ToList();
            library.Media = media;

            await _unitOfWork.SeriesLibraries.AddOrUpdateInclusive(library);
            await _unitOfWork.Complete();

            return library;
        }

        public Task<IEnumerable<SeriesLibrary>> Get() {
            throw new System.NotImplementedException();
        }

        public Task<SeriesLibrary> Get(int id) {
            throw new System.NotImplementedException();
        }

        public async Task Delete(Library library) {
            _unitOfWork.Genres.RemoveRange(_unitOfWork.Genres.Find(g => g.ItemGenres.Any(ig => ig.Item.ItemLibraries.All(il => il.LibraryId == library.Id))));
            _unitOfWork.Items.RemoveRange(library.ItemLibraries.Where(il => il.Item.ItemLibraries.All(l => l.LibraryId == library.Id)).Select(il => il.Item));
            _unitOfWork.SeriesLibraries.Remove((SeriesLibrary)library);
            await _unitOfWork.Complete();
        }
    }
}
