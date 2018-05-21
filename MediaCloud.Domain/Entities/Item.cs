using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {

    public abstract class Item {

        public int Id { get; set; }
        public string Title { get; set; }
	    public string PosterPath { get; set; }
        public string BackdropPath { get; set; }

		public virtual IEnumerable<ItemLibrary> ItemLibraries { get; set; }

        protected Item() { }

        protected Item(SearchBase apiItem) {
            Id = apiItem.Id;
        }
    }
}
