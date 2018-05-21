using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Item {

    public interface IItemRepository : IRepository<Entities.Item> {

    }

    public class ItemRepository : Repository<Entities.Item>, IItemRepository {

        public ItemRepository(DbContext context) : base(context) { }

        public override Task AddOrUpdateInclusive(Entities.Item entity) {
            throw new System.NotImplementedException();
        }
    }
}
