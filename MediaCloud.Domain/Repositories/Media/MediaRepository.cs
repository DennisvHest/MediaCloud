
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Media {

    public interface IMediaRepository : IRepository<Entities.Media> {

    }

    public class MediaRepository : Repository<Entities.Media>, IMediaRepository {

        public MediaRepository(DbContext context) : base(context) { }
        public override Task AddOrUpdateInclusive(Entities.Media entity) {
            throw new System.NotImplementedException();
        }
    }
}
