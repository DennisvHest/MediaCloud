using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/items")]
  public class ItemController : Controller {

    private readonly IItemService<Item> _itemService;
    private readonly IEpisodeService _episodeService;

    public ItemController(IItemService<Item> itemService, IEpisodeService episodeService) {
      _itemService = itemService;
      _episodeService = episodeService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
      Item item = await _itemService.Get(id);

      if (item == null)
        return NotFound();

      if (item is Movie movie) {
        return Ok(new ApiMovie(movie));
      }

      return Ok(new ApiSeries((Series)item));
    }

    [HttpGet("autocomplete")]
    public IEnumerable<AutocompleteItem> Autocomplete(string query) {
      Task<IEnumerable<Item>> foundItems = _itemService.Search(query);
      Task<IEnumerable<Episode>> foundEpisodes = _episodeService.Search(query);

      Task.WaitAll(foundItems, foundEpisodes);

      List<AutocompleteItem> autocompleteItems = new List<AutocompleteItem>(foundItems.Result.Select(i => new AutocompleteItem(i)));
      autocompleteItems.AddRange(foundEpisodes.Result.Select(e => new AutocompleteItem(e)));

      return autocompleteItems;
    }
  }
}
