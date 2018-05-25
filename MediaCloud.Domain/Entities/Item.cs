using System;
using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {

    public abstract class Item {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public string BackdropPath { get; set; }

        public virtual ICollection<ItemGenre> ItemGenres { get; set; }
		public virtual ICollection<ItemLibrary> ItemLibraries { get; set; }

        protected Item() { }

        protected Item(SearchMovieTvBase apiItem) {
            Id = apiItem.Id;
            Description = apiItem.Overview;
            PosterPath = apiItem.PosterPath;
            BackdropPath = apiItem.BackdropPath;
        }
    }
}
