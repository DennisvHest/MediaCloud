using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MediaCloud.Services {

    public interface IEpisodeService {
        Task<Episode> Get(int id);
        Task<IEnumerable<Episode>> Search(string query);
    }

    public class EpisodeService : IEpisodeService {

        private readonly IUnitOfWork _unitOfWork;

        public EpisodeService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Task<Episode> Get(int id) {
            return _unitOfWork.Episodes.Get(id);
        }

        public Task<IEnumerable<Episode>> Search(string query) {
            return Task.FromResult(_unitOfWork.Episodes.Find(e => EF.Functions.Like(e.Title, $"%{query}%")));
        }
    }
}
