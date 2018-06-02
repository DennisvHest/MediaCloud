using System.Threading.Tasks;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/episodes")]
  public class EpisodeController : Controller {

    private readonly IEpisodeService _episodeService;

    public EpisodeController(IEpisodeService episodeService) {
      _episodeService = episodeService;
    }

    [HttpGet("{id}")]
    public async Task<ApiEpisode> Get(int id) {
      return new ApiEpisode(await _episodeService.Get(id), true);
    }
  }
}
