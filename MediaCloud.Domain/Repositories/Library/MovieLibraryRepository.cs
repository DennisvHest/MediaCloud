using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Library {

	public interface IMovieLibraryRepository : IRepository<MovieLibrary> {
		Task<IEnumerable<MovieLibrary>> GetAllIncludingMovies();
		Task<MovieLibrary> GetIncludingMovies(int id);
	}

	public class MovieLibraryRepository : Repository<MovieLibrary>, IMovieLibraryRepository {

		public MovieLibraryRepository(MediaCloudContext context) : base(context) { }

		public async Task<IEnumerable<MovieLibrary>> GetAllIncludingMovies() {
			return await MediaCloudContext.MovieLibraries
					.Include(l => l.ItemLibraries)
					.ThenInclude(il => il.Item)
					.ToListAsync();
		}

		public async Task<MovieLibrary> GetIncludingMovies(int id) {
			return await MediaCloudContext.MovieLibraries
				.Include(l => l.ItemLibraries)
				.ThenInclude(il => il.Item)
				.SingleOrDefaultAsync(l => l.Id == id);
		}

	    public override async Task AddOrUpdateInclusive(MovieLibrary library) {
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

	        await MediaCloudContext.MovieLibraries.AddAsync(library);
	        await MediaCloudContext.SaveChangesAsync();
	    }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
	}
}
