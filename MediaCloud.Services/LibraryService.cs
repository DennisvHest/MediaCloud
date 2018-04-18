using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Services {

    public interface ILibraryService<T> where T : Item {
        Task<Library<Movie>> Create(string name, string folderPath);
        IEnumerable<Library<T>> Get();
        Library<T> Get(int id);
    }

    public abstract class LibraryService<T> : ILibraryService<T> where T : Item {

        private readonly List<Library<T>> _libraries;

        protected LibraryService() {
            _libraries = new List<Library<T>>();
        }

        public abstract Task<Library<Movie>> Create(string name, string folderPath);

        public IEnumerable<Library<T>> Get() {
            return _libraries;
        }

        public Library<T> Get(int id) {
            return _libraries.FirstOrDefault(l => l.Id == id);
        }
    }
}
