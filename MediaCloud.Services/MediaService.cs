using System.IO;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface IMediaService {
        Task<Media> Get(int id);
        Task<Stream> GetStream(Media media);
    }

    public class MediaService : IMediaService {

        private readonly IUnitOfWork _unitOfWork;

        public MediaService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Media> Get(int id) {
            return await _unitOfWork.Media.Get(id);
        }

        public async Task<Stream> GetStream(Media media) {
            return File.OpenRead(media.FileLocation);
        }
    }
}
