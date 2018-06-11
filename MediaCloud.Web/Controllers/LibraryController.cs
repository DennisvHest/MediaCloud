using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Common.Enums;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/libraries")]
  public class LibraryController : Controller {

    private readonly ILibraryService<Library> _libraryService;
    private readonly ILibraryService<MovieLibrary> _movieLibraryService;
    private readonly ILibraryService<SeriesLibrary> _seriesLibraryService;

    public LibraryController(ILibraryService<Library> libraryService, ILibraryService<MovieLibrary> movieLibraryService, ILibraryService<SeriesLibrary> seriesLibraryService) {
      _libraryService = libraryService;
      _movieLibraryService = movieLibraryService;
      _seriesLibraryService = seriesLibraryService;
    }

    [HttpGet]
    public async Task<IActionResult> Get() {
      IEnumerable<Library> libraries = await _libraryService.Get();

      return Ok(libraries.Select(l => new ApiLibrary(l)));
    }

    [HttpGet("list")]
    public async Task<IActionResult> List() {
      IEnumerable<Library> libraries = await _libraryService.Get();

      return Ok(libraries.Select(l => new ApiLibrary(l, false)));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      Library library = await _libraryService.Get(id);

      if (library == null)
        return NotFound();

      return Ok(new ApiLibrary(library));
    }

    [HttpGet("{id}/items")]
    public async Task<IActionResult> GetItems(int id) {
      Library library = await _libraryService.Get(id);

      if (library == null)
        return NotFound();

      return Ok(new ApiLibrary(library, itemsOnly: true));
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
