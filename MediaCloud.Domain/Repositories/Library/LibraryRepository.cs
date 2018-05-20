using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Library {

	public interface ILibraryRepository : IRepository<Entities.Library> { }

	public class LibraryRepository : Repository<Entities.Library>, ILibraryRepository {

	    public LibraryRepository(MediaCloudContext context) : base(context) { }

	    public async Task<Entities.Library> GetIncludingItems(int id) {
			return await MediaCloudContext.Libraries.SingleOrDefaultAsync(l => l.Id == id);
		}

	    public override Task AddOrUpdateInclusive(Entities.Library entity) {
	        throw new System.NotImplementedException();
	    }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
	}
}
