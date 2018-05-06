using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {
    public class Movie : Item {

        public Movie() { }

        public Movie(SearchMovie apiMovie) : base(apiMovie) {
            Title = apiMovie.Title;
	        PosterPath = apiMovie.PosterPath;
        }
    }
}
