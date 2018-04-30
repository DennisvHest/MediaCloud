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
      Items = library.ItemLibraries.Select(il => new ApiItem(il.Item));
    }
  }
}
