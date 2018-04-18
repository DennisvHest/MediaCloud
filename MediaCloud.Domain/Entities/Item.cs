using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {

    public abstract class Item {

        public int Id { get; set; }
        public string Title { get; set; }

        protected Item() { }

        protected Item(SearchBase apiItem) {
            Id = apiItem.Id;
        }
    }
}
