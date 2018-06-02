using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiMovie : ApiItem {

    public IEnumerable<ApiMedia> Media { get; set; }

    public ApiMovie(Movie movie) : base(movie) {
      Media = movie.Media.Select(m => new ApiMedia(m));
    }
  }
}
