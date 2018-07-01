using System.Collections.Generic;
using System.Diagnostics;
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
            IList<ItemLibrary> existingItemLibraries = await MediaCloudContext.ItemLibraries.ToListAsync();
            IList<ItemGenre> existingItemGenres = await MediaCloudContext.ItemGenres.ToListAsync();
            IList<Entities.Episode> existingEpisodes = await MediaCloudContext.Episodes.ToListAsync();

            // Don't re-add existing items
            foreach (ItemLibrary itemLibrary in library.ItemLibraries) {
                ItemLibrary existingItemLibrary = existingItemLibraries.FirstOrDefault(x => x.ItemId == itemLibrary.Item.Id);

                if (existingItemLibrary != null) {
                    MediaCloudContext.Entry(itemLibrary.Item).State = EntityState.Unchanged;
                    itemLibrary.ItemId = existingItemLibrary.ItemId;
                } else {
                    existingItemLibraries.Add(itemLibrary);
                }

                // Don't re-add existing genres
                foreach (ItemGenre itemGenre in itemLibrary.Item.ItemGenres) {
                    ItemGenre existingItemGenre =
                        existingItemGenres.FirstOrDefault(x => x.GenreId == itemGenre.Genre.Id);

                    if (existingItemGenre != null) {
                        Entities.Genre localEntry = MediaCloudContext.Set<Entities.Genre>().Local.FirstOrDefault(entry => entry.Id == itemGenre.Genre.Id);

                        //Make sure an already attached genre isn't attached again
                        if (localEntry != null)
                            MediaCloudContext.Entry(localEntry).State = EntityState.Detached;

                        MediaCloudContext.Entry(itemGenre.Genre).State = EntityState.Unchanged;
                        itemGenre.GenreId = existingItemGenre.GenreId;
                    } else {
                        existingItemGenres.Add(itemGenre);
                    }
                }

                foreach (Entities.Season season in ((Entities.Series) itemLibrary.Item).Seasons) {
                    // Don't re-add existing episodes
                    foreach (Entities.Episode episode in season.Episodes) {
                        Entities.Episode existingEpisodey = existingEpisodes.FirstOrDefault(x => x.Id == episode.Id);

                        if (existingEpisodey != null) {
                            Entities.Episode localEntry = MediaCloudContext.Set<Entities.Episode>().Local.FirstOrDefault(entry => entry.Id == episode.Id);

                            //Make sure an already attached genre isn't attached again
                            if (localEntry != null)
                                MediaCloudContext.Entry(localEntry).State = EntityState.Detached;

                            MediaCloudContext.Entry(episode).State = EntityState.Unchanged;
                        } else {
                            existingEpisodes.Add(episode);
                        }
                    }
                }
            }

            await MediaCloudContext.SeriesLibraries.AddAsync(library);
            await MediaCloudContext.SaveChangesAsync();
        }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
    }
}
