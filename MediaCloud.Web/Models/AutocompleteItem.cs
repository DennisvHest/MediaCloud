using MediaCloud.Domain.Entities;

namespace MediaCloud.Web.Models {

  public class AutocompleteItem {

    public string Id { get; set; }
    public string Text { get; set; }

    public AutocompleteItem(Item item) {
      Id = item.Id.ToString();
      Text = item.Title;
    }

    public AutocompleteItem(Episode episode) {
      Id = $"{episode.Season?.Series.Id}_{episode.Id}";
      Text = episode.Title;
    }
  }
}
