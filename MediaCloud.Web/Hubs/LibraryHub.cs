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

    public async Task<int?> Create(LibraryType type, string name, string folderPath) {
      void ProgressReportCallback(int percentage, string message) {
        Clients.Caller.SendAsync("progressReport", new { percentage, message });
      }

      Library newLibrary = null;

      switch (type) {
        case LibraryType.Movies:
        newLibrary = await _movieLibraryService.Create(name, folderPath, ProgressReportCallback);
        break;
        case LibraryType.Series:
        newLibrary = await _seriesLibraryService.Create(name, folderPath, ProgressReportCallback);
        break;
      }

      return newLibrary?.Id;
    }
  }
}
