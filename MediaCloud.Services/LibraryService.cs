using System.Collections.Generic;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Services {

    public interface ILibraryService<T> where T : Item {
        IEnumerable<Library<T>> Get();
        void Add(Library<T> library);
    }

    public class LibraryService<T> : ILibraryService<T> where T : Item {

        private readonly List<Library<T>> _libraries;

        public LibraryService() {
            _libraries = new List<Library<T>>();
        }

        public void Add(Library<T> library) {
            _libraries.Add(library);
        }

        public IEnumerable<Library<T>> Get() {
            return _libraries;
        }
    }
}
