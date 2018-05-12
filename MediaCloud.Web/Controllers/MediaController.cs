using System.Threading.Tasks;
using MediaCloud.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/[controller]")]
  public class MediaController : Controller {

    private readonly IMediaService _mediaService;

    public MediaController(IMediaService mediaService) {
      _mediaService = mediaService;
    }

    [HttpGet("{id}/stream")]
    public async Task<FileStreamResult> Get(int id) {
      return new FileStreamResult(await _mediaService.GetStream(id), "video/mp4");
    }
  }
}
