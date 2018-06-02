using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiEpisode {

    public int Id { get; set; }
    public int EpisodeNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StillPath { get; set; }
    public ApiSeason Season { get; set; }
    public IEnumerable<ApiMedia> Media { get; set; }

    public ApiEpisode(Episode episode, bool backInclusive = false) {
      Id = episode.Id;
      EpisodeNumber = episode.EpisodeNumber;
      Title = episode.Title;
      Description = episode.Description;
      StillPath = episode.StillPath;

      if (backInclusive)
        Season = new ApiSeason(episode.Season, true, false);

      Media = episode.Media.Select(m => new ApiMedia(m));
    }
  }
}
