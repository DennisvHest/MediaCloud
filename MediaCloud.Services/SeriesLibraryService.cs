using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public async Task<SeriesLibrary> Create(string name, string folderPath) {
            List<Series> series = new List<Series>();
            List<Media> media = new List<Media>();

            //Find video files
            List<string> itemFiles = Directory.GetFileSystemEntries(folderPath, "*.mkv", SearchOption.AllDirectories).ToList();
            itemFiles.AddRange(Directory.GetFileSystemEntries(folderPath, "*.mp4", SearchOption.AllDirectories));

            //Group series by title
            Regex regex = new Regex(@"(.+)\s+?(S[0-9]{1,2}E[0-9]{1,2})");

            IEnumerable<Match> fileNameMatches = itemFiles.Select(f => regex.Match(Path.GetFileNameWithoutExtension(new FileInfo(f).Name)));

            IEnumerable<IGrouping<string, IEnumerable<string>>> groupedSeries = fileNameMatches.GroupBy(m => 
                m.Groups[1].Value, 
                m => fileNameMatches.Where(ma => ma.Groups[1].Value == m.Groups[1].Value)
                    .Select(mat => mat.Groups[2].Value));

            foreach (IGrouping<string, IEnumerable<string>> seriesFile in groupedSeries) {
                //Search series in API
                IEnumerable<Series> foundSeries = await _seriesApiRepository.SearchSeries(seriesFile.Key);

                if (foundSeries.Any()) {
                    series.Add(foundSeries.FirstOrDefault());
                }
            }

            series = series.DistinctBy(s => s.Id).ToList();

            SeriesLibrary library = new SeriesLibrary { Name = name };
            library.ItemLibraries = series.Select(m => new ItemLibrary { Item = m, Library = library }).ToList();

            _unitOfWork.SeriesLibraries.AddOrUpdateInclusive(library);
            await _unitOfWork.Complete();

            return library;
        }

        public Task<IEnumerable<SeriesLibrary>> Get() {
            throw new System.NotImplementedException();
        }

        public Task<SeriesLibrary> Get(int id) {
            throw new System.NotImplementedException();
        }
    }
}
