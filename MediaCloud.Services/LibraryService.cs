﻿using System;
using System.Collections.Generic;
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
        Task<T> Create(string name, string folderPath);
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
    }

    public class LibraryService : ILibraryService<Library> {

        private readonly IUnitOfWork _unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public Task<Library> Create(string name, string folderPath) {
            throw new NotImplementedException("Not implemented yet.");
        }

        public async Task<IEnumerable<Library>> Get() {
            return await _unitOfWork.Libraries.GetAllIncludingItems();
        }

        public async Task<Library> Get(int id) {
            return await _unitOfWork.Libraries.GetIncludingItems(id);
        }
    }
}