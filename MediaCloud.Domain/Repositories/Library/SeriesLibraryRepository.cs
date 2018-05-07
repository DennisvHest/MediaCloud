using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Domain.Repositories.Library {

    public interface ISeriesLibraryRepository : IRepository<SeriesLibrary> {
        Task<IEnumerable<SeriesLibrary>> GetAllIncludingSeries();
        Task<SeriesLibrary> GetIncludingSeries(int id);
    }

    public class SeriesLibraryRepository : Repository<SeriesLibrary>, ISeriesLibraryRepository {

        public SeriesLibraryRepository(MediaCloudContext context) : base(context) { }

        public Task<IEnumerable<SeriesLibrary>> GetAllIncludingSeries() {
            throw new System.NotImplementedException();
        }

        public Task<SeriesLibrary> GetIncludingSeries(int id) {
            throw new System.NotImplementedException();
        }

        public override async Task AddOrUpdateInclusive(SeriesLibrary library) {
            IList<ItemLibrary> existingItemLibraries = MediaCloudContext.ItemLibraries.ToList();

            foreach (ItemLibrary itemLibrary in library.ItemLibraries) {
                ItemLibrary existingItemLibrary = existingItemLibraries.FirstOrDefault(x => x.ItemId == itemLibrary.Item.Id);

                if (existingItemLibrary != null) {
                    itemLibrary.Item = null;
                    itemLibrary.ItemId = existingItemLibrary.ItemId;
                } else {
                    existingItemLibraries.Add(itemLibrary);
                }
            }

            await MediaCloudContext.SeriesLibraries.AddAsync(library);
            await MediaCloudContext.SaveChangesAsync();
        }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
    }
}
