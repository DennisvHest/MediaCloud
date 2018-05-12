using System.IO;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface IMediaService {
        Task<Stream> GetStream(int mediaId);
    }

    public class MediaService : IMediaService {

        private readonly IUnitOfWork _unitOfWork;

        public MediaService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task<Stream> GetStream(int mediaId) {
            Media media = await _unitOfWork.Media.Get(mediaId);

            return File.OpenRead(media.FileLocation);
        }
    }
}
