using System;
using System.Threading.Tasks;
using MediaCloud.Common.Enums;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using Microsoft.AspNetCore.SignalR;

namespace MediaCloud.Web.Hubs {

  public class LibraryHub : Hub {

    private readonly ILibraryService<MovieLibrary> _movieLibraryService;
    private readonly ILibraryService<SeriesLibrary> _seriesLibraryService;

    public LibraryHub(ILibraryService<MovieLibrary> movieLibraryService, ILibraryService<SeriesLibrary> seriesLibraryService) {
      _movieLibraryService = movieLibraryService;
      _seriesLibraryService = seriesLibraryService;
    }

    public async Task Create(LibraryType type, string name, string folderPath) {
      void ProgressReportCallback(int progressPercentage) {
        Clients.Caller.SendAsync("progressReport", new { progressPercentage });
      }

      Library newLibrary = null;

      switch (type) {
        //        case LibraryType.Movies:
        //        Task.Run(() => {
        //          newLibrary = _movieLibraryService.Create(name, folderPath, progressReportCallback);
        //        });
        //        break;
        case LibraryType.Series:
        newLibrary = await _seriesLibraryService.Create(name, folderPath, ProgressReportCallback);
        break;
      }

      await Clients.Caller.SendAsync("libraryCreated", new { libraryId = newLibrary?.Id });
    }
  }
}
