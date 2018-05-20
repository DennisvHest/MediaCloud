using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiItem {

    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterPath { get; set; }
    public string ItemType { get; set; }

    public ApiItem(Item item) {
      Id = item.Id;
      Title = item.Title;
      PosterPath = item.PosterPath;

      ItemType = item is Movie ? typeof(Movie).Name : typeof(Series).Name;
    }
  }
}
