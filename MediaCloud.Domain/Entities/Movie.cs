using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {
    public class Movie : Item {

        public virtual ICollection<Media> Media { get; set; }

        public Movie() { }

        public Movie(SearchMovie apiMovie) : base(apiMovie) {
            Title = apiMovie.Title;
	        PosterPath = apiMovie.PosterPath;
            BackdropPath = apiMovie.BackdropPath;
        }
    }
}
