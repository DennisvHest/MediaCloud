using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Services {

    public interface IItemService<T> where T : Item {

		/// <summary>
		/// Searches for items in the database or API with the given query.
		/// </summary>
		/// <param name="query">The title of the item.</param>
		/// <returns>The found items.</returns>
        Task<IEnumerable<T>> Search(string query);
        Task<T> Add(Movie movie);
    }

    public abstract class ItemService<T> : IItemService<T> where T : Item {

        public abstract Task<IEnumerable<T>> Search(string query);
        public abstract Task<T> Add(Movie movie);
    }
}
