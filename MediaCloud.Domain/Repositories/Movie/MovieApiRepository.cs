using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaCloud.Domain.Repositories.Movie {

    public interface IMovieApiRepository {
        Task<IEnumerable<Entities.Movie>> SearchMovie(string query);
    }
}
