using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Common.Enums;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class LibrariesController : Controller {

    private readonly ILibraryService<Library> _libraryService;
    private readonly ILibraryService<MovieLibrary> _movieLibraryService;
    private readonly ILibraryService<SeriesLibrary> _seriesLibraryService;

    public LibrariesController(ILibraryService<Library> libraryService, ILibraryService<MovieLibrary> movieLibraryService, ILibraryService<SeriesLibrary> seriesLibraryService) {
      _libraryService = libraryService;
      _movieLibraryService = movieLibraryService;
      _seriesLibraryService = seriesLibraryService;
    }

    [HttpGet]
    public async Task<IEnumerable<ApiLibrary>> Get() {
      IEnumerable<Library> libraries = await _libraryService.Get();

      return libraries.Select(l => new ApiLibrary(l));
    }

    [HttpGet("{id}")]
    public async Task<ApiLibrary> Get(int id) {
      return new ApiLibrary(await _libraryService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(LibraryType type, string name, string folderPath) {
      Library newLibrary = null;

      switch (type) {
        case LibraryType.Movies:
          newLibrary = await _movieLibraryService.Create(name, folderPath);
          break;
        case LibraryType.Series:
          newLibrary = await _seriesLibraryService.Create(name, folderPath);
          break;
      }

      return CreatedAtAction("Get", "Libraries", new { id = newLibrary?.Id });
    }
  }
}