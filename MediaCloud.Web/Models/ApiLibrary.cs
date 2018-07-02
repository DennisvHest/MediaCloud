using System.Collections.Generic;
using System.Linq;
using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class ApiLibrary {

    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<ApiItem> Items { get; set; }
    public IEnumerable<ApiMedia> Media { get; set; }
    public string LibraryType { get; }

    public ApiLibrary(Library library, bool inclusive = true, bool itemsOnly = false, bool mediaOnly = false) {
      LibraryType = library is MovieLibrary ? typeof(MovieLibrary).Name : typeof(SeriesLibrary).Name;

      Id = library.Id;
      Name = library.Name;

      if (inclusive) {
        if (!mediaOnly) {
          Items = library.ItemLibraries.Select<ItemLibrary, ApiItem>(il => {
            if (il.Item is Movie movie) {
              return new ApiMovie(movie);
            }

            return new ApiSeries((Series)il.Item, !itemsOnly);
          });
        }

        Media = library.Media.Select(m => new ApiMedia(m, mediaOnly));
      }
    }
  }
}
