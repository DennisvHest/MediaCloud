using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiEpisode {

    public int Id { get; set; }
    public int EpisodeNumber { get; set; }
    public string Title { get; set; }

    public ApiEpisode(Episode episode) {
      Id = episode.Id;
      EpisodeNumber = episode.EpisodeNumber;
      Title = episode.Title;
    }
  }
}
