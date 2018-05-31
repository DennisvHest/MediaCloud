using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface ISeasonService {
        Task<Season> Get(int id);
    }

    public class SeasonService : ISeasonService {

        private readonly IUnitOfWork _unitOfWork;

        public SeasonService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Season> Get(int id) {
            return await _unitOfWork.Seasons.Get(id);
        }
    }
}
