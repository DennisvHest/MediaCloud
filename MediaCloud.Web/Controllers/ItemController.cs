using System.Threading.Tasks;
using MediaCloud.Domain.Entities;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/items")]
  public class ItemController : Controller {

    private readonly IItemService<Item> _itemService;

    public ItemController(IItemService<Item> itemService) {
      _itemService = itemService;
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
  }
}
