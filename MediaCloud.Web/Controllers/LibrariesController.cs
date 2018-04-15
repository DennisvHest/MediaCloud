using System.Collections.Generic;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class LibrariesController : Controller {

    private readonly ILibraryService<Item> _libraryService;

    public LibrariesController() {
      _libraryService = new LibraryService<Item>();

      _libraryService.Add(new Library<Item> {
        Name = "Movies",
        Items = new List<Item> {
          new Movie { Id = 1, Name = "Black Panther" }
        }
      });

      _libraryService.Add(new Library<Item> {
        Name = "Movies2",
        Items = new List<Item> {
          new Movie { Id = 2, Name = "Ready Player One" }
        }
      });
    }

    [HttpGet]
    public IEnumerable<Library<Item>> Get() {
      return _libraryService.Get();
    }
  }
}
