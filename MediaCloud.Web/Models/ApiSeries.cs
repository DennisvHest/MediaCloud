using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiSeries : ApiItem {

    public IEnumerable<ApiSeason> Seasons { get; set; }

    public ApiSeries(Series series) : base(series) {
      Seasons = series.Seasons.Select(s => new ApiSeason(s));
    }
  }
}
