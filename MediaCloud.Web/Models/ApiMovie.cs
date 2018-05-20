using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiMovie : ApiItem {

    public ApiMovie(Movie movie) : base(movie) { }
  }
}
