using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Domain.Repositories.Movie {

    public interface IMovieRepository : IRepository<Entities.Movie> {

    }

    public class MovieRepository : Repository<Entities.Movie>, IMovieRepository {

        public MovieRepository(DbContext context) : base(context) { }

	    public override Task AddOrUpdateInclusive(Entities.Movie entity) {
		    throw new NotImplementedException();
	    }
    }
}
