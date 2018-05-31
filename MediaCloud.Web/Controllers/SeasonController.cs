using System.Threading.Tasks;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/seasons")]
  public class SeasonController : Controller {

    private readonly ISeasonService _seasonService;

    public SeasonController(ISeasonService seasonService) {
      _seasonService = seasonService;
    }

    [HttpGet("{id}")]
    public async Task<ApiSeason> Get(int id) {
      return new ApiSeason(await _seasonService.Get(id), true);
    }
  }
}
