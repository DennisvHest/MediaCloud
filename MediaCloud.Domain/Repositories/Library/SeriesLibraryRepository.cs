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

        public override Task AddOrUpdateInclusive(SeriesLibrary entity) {
            throw new System.NotImplementedException();
        }

        private MediaCloudContext MediaCloudContext => Context as MediaCloudContext;
    }
}
