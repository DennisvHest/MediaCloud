using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class LibrariesController : Controller {

    private readonly ILibraryService<MovieLibrary> _movieLibraryService;

    public LibrariesController(ILibraryService<MovieLibrary> movieLibraryService) {
      _movieLibraryService = movieLibraryService;
    }

    [HttpGet]
    public async Task<IEnumerable<ApiLibrary>> Get() {
      IEnumerable<MovieLibrary> libraries = await _movieLibraryService.Get();

      return libraries.Select(l => new ApiLibrary(l));
    }

    [HttpGet("{id}")]
    public async Task<ApiLibrary> Get(int id) {
      return new ApiLibrary(await _movieLibraryService.Get(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string folderPath) {
      MovieLibrary newLibrary = await _movieLibraryService.Create(name, folderPath);

      return CreatedAtAction("Get", "Libraries", new { id = newLibrary?.Id });
    }
  }
}
