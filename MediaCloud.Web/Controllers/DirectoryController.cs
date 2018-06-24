using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaCloud.Services;
using MediaCloud.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaCloud.Web.Controllers {

  [Route("api/directories")]
  public class DirectoryController : Controller {

    private readonly IFileExplorationService _fileExplorationService;

    public DirectoryController(IFileExplorationService fileExplorationService) {
      _fileExplorationService = fileExplorationService;
    }

    [HttpGet("drives")]
    public IEnumerable<ApiDirectory> DrivesList() {
      IEnumerable<DriveInfo> drives = _fileExplorationService.GetDrives();

      return drives.Select(d => new ApiDirectory(d));
    }

    [HttpGet]
    public IEnumerable<ApiDirectory> Get(string path) {
      IEnumerable<DirectoryInfo> directories = _fileExplorationService.GetSubDirectories(path);

      return directories.Select(d => new ApiDirectory(d));
    }
  }
}
