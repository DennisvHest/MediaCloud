using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MediaCloud.Domain.Repositories.Library {

    public interface ILibraryRepository : IRepository<Entities.Library> {
        IEnumerable<Entities.Library> GetHomeLibraries();
    }

	public class LibraryRepository : Repository<Entities.Library>, ILibraryRepository {

	    public LibraryRepository(MediaCloudContext context) : base(context) { }

	    public async Task<Entities.Library> GetIncludingItems(int id) {
			return await MediaCloudContext.Libraries.SingleOrDefaultAsync(l => l.Id == id);
		}

	    public override Task AddOrUpdateInclusive(Entities.Library entity) {
	        throw new System.NotImplementedException();
	    }

	    public IEnumerable<Entities.Library> GetHomeLibraries() {
	        DbSet<Entities.Library> libraries = MediaCloudContext.Libraries;

	        foreach (Entities.Library library in libraries) {
	            library.Media = library.Media.Where(m => m.MovieId != null || m.EpisodeId != null).OrderByDescending(m => m.DateAdded).Take(6).ToList();
	        }

	        return libraries;
	    }

	    private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
	}
}
