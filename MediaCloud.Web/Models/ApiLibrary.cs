using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiLibrary {

    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ApiItem> Items { get; set; }

    public ApiLibrary(Library library) {
      Id = library.Id;
      Name = library.Name;
      Items = library.ItemLibraries.Select<ItemLibrary, ApiItem>(il => {
        if (il.Item is Movie movie) {
          return new ApiMovie(movie);
        }

        return new ApiSeries((Series)il.Item);
      });
    }
  }
}
