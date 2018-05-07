using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {

	public class Series : Item {

        public virtual ICollection<Season> Seasons { get; set; }

        public Series() { }

	    public Series(SearchTv apiSeries) {
	        Id = apiSeries.Id;
	        Title = apiSeries.Name;
	        PosterPath = apiSeries.PosterPath;
	    }
	}
}
