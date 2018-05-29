using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Services {

    public interface IItemService<T> where T : Item {

        Task<T> Get(int id);
        /// <summary>
        /// Searches for items in the database or API with the given query.
        /// </summary>
        /// <param name="query">The title of the item.</param>
        /// <returns>The found items.</returns>
        Task<IEnumerable<T>> Search(string query);
        Task<T> Add(T movie);
    }

    public abstract class ItemService<T> : IItemService<T> where T : Item {

        public abstract Task<T> Get(int id);
        public abstract Task<IEnumerable<T>> Search(string query);
        public abstract Task<T> Add(T movie);
    }

    public class ItemServiceConcrete : ItemService<Item> {

        private readonly IUnitOfWork _unitOfWork;

        public ItemServiceConcrete(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public override async Task<Item> Get(int id) {
            return await _unitOfWork.Items.Get(id);
        }

        public override async Task<IEnumerable<Item>> Search(string query) {
            return await Task.FromResult(_unitOfWork.Items.Find(i => EF.Functions.Like(i.Title, $"%{query}%")));
        }

        public override Task<Item> Add(Item movie) {
            throw new System.NotImplementedException();
        }
    }
}
