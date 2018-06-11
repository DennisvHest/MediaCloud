using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiSeries : ApiItem {

    public IEnumerable<ApiSeason> Seasons { get; set; }
    public int SeasonCount { get; set; }

    public ApiSeries(Series series, bool inclusive = true) : base(series) {
      SeasonCount = series.Seasons.Count;

      if (inclusive)
        Seasons = series.Seasons.Select(s => new ApiSeason(s));
    }
  }
}
