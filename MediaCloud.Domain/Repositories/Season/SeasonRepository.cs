using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Season {

    public interface ISeasonRepository : IRepository<Entities.Season> {

    }

    public class SeasonRepository : Repository<Entities.Season>, ISeasonRepository {

        public SeasonRepository(DbContext context) : base(context) { }

        public override Task AddOrUpdateInclusive(Entities.Season entity) {
            throw new NotImplementedException();
        }
    }
}
