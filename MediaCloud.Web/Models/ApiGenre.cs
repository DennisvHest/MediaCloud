using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiGenre {

    public int Id { get; set; }
    public string Name { get; set; }

    public ApiGenre(Genre genre) {
      Id = genre.Id;
      Name = genre.Name;
    }
  }
}
