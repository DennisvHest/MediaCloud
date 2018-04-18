using System.Collections.Generic;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class LibrariesController : Controller {

    private readonly ILibraryService<Movie> _movieLibraryService;

    public LibrariesController(ILibraryService<Movie> movieLibraryService) {
      _movieLibraryService = movieLibraryService;
    }

    [HttpGet]
    public IEnumerable<Library<Movie>> Get() {
      return _movieLibraryService.Get();
    }

    [HttpGet("{id}")]
    public Library<Movie> Get(int id) {
      return _movieLibraryService.Get(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name, string folderPath) {
      Library<Movie> newLibrary = await _movieLibraryService.Create(name, folderPath);

      return CreatedAtAction("Get", "Libraries", new { id = newLibrary?.Id });
    }
  }
}
