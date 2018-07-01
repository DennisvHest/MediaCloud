using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Genre {

    public interface IGenreRepository : IRepository<Entities.Genre> {

    }

    public class GenreRepository : Repository<Entities.Genre>, IGenreRepository {

        public GenreRepository(DbContext context) : base(context) { }
        public override Task AddOrUpdateInclusive(Entities.Genre entity) {
            throw new System.NotImplementedException();
        }
    }
}
