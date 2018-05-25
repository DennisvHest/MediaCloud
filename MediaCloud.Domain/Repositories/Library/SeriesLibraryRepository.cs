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
            IList<ItemGenre> existingItemGenres = MediaCloudContext.ItemGenres.ToList();

            //Don't re-add existing items
            foreach (ItemLibrary itemLibrary in library.ItemLibraries) {
                ItemLibrary existingItemLibrary = existingItemLibraries.FirstOrDefault(x => x.ItemId == itemLibrary.Item.Id);

                if (existingItemLibrary != null) {
                    MediaCloudContext.Entry(itemLibrary.Item).State = EntityState.Unchanged;
                    itemLibrary.ItemId = existingItemLibrary.ItemId;
                } else {
                    existingItemLibraries.Add(itemLibrary);
                }

                //Don't re-add existing genres
                foreach (ItemGenre itemGenre in itemLibrary.Item.ItemGenres) {
                    ItemGenre existingItemGenre =
                        existingItemGenres.FirstOrDefault(x => x.GenreId == itemGenre.Genre.Id);

                    if (existingItemGenre != null) {
                        Genre localEntry = MediaCloudContext.Set<Genre>().Local.FirstOrDefault(entry => entry.Id == itemGenre.Genre.Id);

                        //Make sure an already attached genre isn't attached again
                        if (localEntry != null)
                            MediaCloudContext.Entry(localEntry).State = EntityState.Detached;

                        MediaCloudContext.Entry(itemGenre.Genre).State = EntityState.Unchanged;
                        itemGenre.GenreId = existingItemGenre.GenreId;
                    } else {
                        existingItemGenres.Add(itemGenre);
                    }
                }
            }

            await MediaCloudContext.SeriesLibraries.AddAsync(library);
            await MediaCloudContext.SaveChangesAsync();
        }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
    }
}
