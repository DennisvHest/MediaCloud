using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Library {

	public interface ILibraryRepository : IRepository<Entities.Library> {
		Task<IEnumerable<Entities.Library>> GetAllIncludingItems();
		Task<Entities.Library> GetIncludingItems(int id);
	}

	public class LibraryRepository : Repository<Entities.Library>, ILibraryRepository {

		public LibraryRepository(DbContext context) : base(context) { }

		public override Task AddOrUpdateInclusive(Entities.Library entity) {
			throw new System.NotImplementedException();
		}

	    public async Task<IEnumerable<Entities.Library>> GetAllIncludingItems() {
	        return await MediaCloudContext.Libraries
	            .Include(l => l.ItemLibraries)
	            .ThenInclude(il => il.Item)
	            .ToListAsync();
	    }

	    public async Task<Entities.Library> GetIncludingItems(int id) {
			return await MediaCloudContext.Libraries
				.Include(l => l.ItemLibraries)
				.ThenInclude(il => il.Item)
				.SingleOrDefaultAsync(l => l.Id == id);
		}

		private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
	}
}
