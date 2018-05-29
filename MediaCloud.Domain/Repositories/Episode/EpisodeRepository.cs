using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Episode {

    public interface IEpisodeRepository : IRepository<Entities.Episode> {

    }

    public class EpisodeRepository : Repository<Entities.Episode>, IEpisodeRepository {

        public EpisodeRepository(DbContext context) : base(context) { }

        public override Task AddOrUpdateInclusive(Entities.Episode entity) {
            throw new System.NotImplementedException();
        }
    }
}
