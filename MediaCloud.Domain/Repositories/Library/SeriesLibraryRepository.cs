using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Library {

    public interface ISeriesLibraryRepository : IRepository<SeriesLibrary> {

    }

    public class SeriesLibraryRepository : Repository<SeriesLibrary>, ISeriesLibraryRepository {

        public SeriesLibraryRepository(MediaCloudContext context) : base(context) { }

        public override async Task AddOrUpdateInclusive(SeriesLibrary library) {
            IList<ItemLibrary> existingItemLibraries = MediaCloudContext.ItemLibraries.ToList();

            foreach (ItemLibrary itemLibrary in library.ItemLibraries) {
                ItemLibrary existingItemLibrary = existingItemLibraries.FirstOrDefault(x => x.ItemId == itemLibrary.Item.Id);

                if (existingItemLibrary != null) {
                    MediaCloudContext.Entry(itemLibrary.Item).State = EntityState.Unchanged;
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
