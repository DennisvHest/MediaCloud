using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.Search;
using TMDbLib.Objects.TvShows;

namespace MediaCloud.Domain.Entities {

	public class Series : Item {

        public virtual ICollection<Season> Seasons { get; set; }

        public Series() { }

	    public Series(SearchTv apiSeries) : base(apiSeries) {
	        Title = apiSeries.Name;
	        PosterPath = apiSeries.PosterPath;
	    }

	    public Series(SearchTv apiSeries, IEnumerable<TvSeason> apiSeasons) : base(apiSeries) {
	        Title = apiSeries.Name;
	        PosterPath = apiSeries.PosterPath;

	        Seasons = apiSeasons.Select(s => new Season(s)).ToList();
	    }
    }
}
