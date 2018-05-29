using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiSeason {

    public int Id { get; set; }
    public int SeasonNumber { get; set; }
    public string Title { get; set; }
    public string PosterPath { get; set; }

    public IEnumerable<ApiEpisode> Episodes { get; set; }

    public ApiSeason(Season season) {
      Id = season.Id;
      SeasonNumber = season.SeasonNumber;
      Title = season.Title;
      PosterPath = season.PosterPath;

      Episodes = season.Episodes.Select(e => new ApiEpisode(e));
    }
  }
}
