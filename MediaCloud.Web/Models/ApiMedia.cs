using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiMedia {

    public int Id { get; set; }
    public string FileLocation { get; set; }

    public virtual ApiEpisode Episode { get; set; }
    public virtual ApiMovie Movie { get; set; }
    public virtual ApiLibrary Library { get; set; }

    public ApiMedia(Media media, bool backInclusive = false) {
      Id = media.Id;
      FileLocation = media.FileLocation;

      if (backInclusive) {
        if (media.Episode != null)
          Episode = new ApiEpisode(media.Episode, true);

        if (media.Movie != null)
          Movie = new ApiMovie(media.Movie);

        Library = new ApiLibrary(media.Library, false);
      }
    }
  }
}
