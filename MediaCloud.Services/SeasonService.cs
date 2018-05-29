using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface ISeasonService {
    }

    public class SeasonService : ISeasonService {

        private readonly IUnitOfWork _unitOfWork;

        public SeasonService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
    }
}
