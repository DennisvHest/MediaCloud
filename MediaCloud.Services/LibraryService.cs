using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface ILibraryService<T> where T : Library {
        Task<T> Create(string name, string folderPath);
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
    }

    public class LibraryService : ILibraryService<Library> {

        private readonly IUnitOfWork _unitOfWork;

        protected LibraryService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Task<Library> Create(string name, string folderPath) {
            throw new NotImplementedException("Not implemented yet.");
        }

        public async Task<IEnumerable<Library>> Get() {
            return await _unitOfWork.Libraries.GetAll();
        }

        public async Task<Library> Get(int id) {
            return await _unitOfWork.Libraries.GetIncludingItems(id);
        }
    }
}
