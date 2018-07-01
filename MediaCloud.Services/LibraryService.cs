using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Domain.Repositories;

namespace MediaCloud.Services {

    public interface ILibraryService<T> where T : Library {
		/// <summary>
		/// Creates a new library using the files in the given folderPath. Metadata of the files 
		/// will be retrieved from the API.
		/// </summary>
		/// <param name="name">Name of the new library.</param>
		/// <param name="folderPath">Path to the folder with files to include in the new library.</param>
		/// <returns></returns>
        Task<T> Create(string name, string folderPath, Action<int, string> progressReportCallback);
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task Delete(Library library);
    }

    public class LibraryService : ILibraryService<Library> {

        private readonly IUnitOfWork _unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Task<Library> Create(string name, string folderPath, Action<int, string> progressReportCallback) {
            throw new NotImplementedException("Not implemented yet.");
        }

        public async Task<IEnumerable<Library>> Get() {
            return await _unitOfWork.Libraries.GetAll();
        }

        public async Task<Library> Get(int id) {
            return await _unitOfWork.Libraries.Get(id);
        }

        public async Task Delete(Library library) {
            _unitOfWork.Genres.RemoveRange(_unitOfWork.Genres.Find(g => g.ItemGenres.Any(ig => !ig.Item.ItemLibraries.Any(il => il.LibraryId != library.Id))));
            _unitOfWork.Items.RemoveRange(library.ItemLibraries.Where(il => !il.Item.ItemLibraries.Any(l => l.LibraryId != library.Id)).Select(il => il.Item));
            _unitOfWork.Libraries.Remove(library);
            await _unitOfWork.Complete();
        }
    }
}
