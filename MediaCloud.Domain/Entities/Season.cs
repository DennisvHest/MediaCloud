using System.Collections.Generic;
using System.Linq;
using TMDbLib.Objects.TvShows;

namespace MediaCloud.Domain.Entities {

    public class Season {

        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public string Title { get; set; }
        public string PosterPath { get; set; }

        public Season() { }

        public Season(TvSeason apiSeason) {
            Id = apiSeason.Id.Value;
            SeasonNumber = apiSeason.SeasonNumber;
            Title = apiSeason.Name;
            PosterPath = apiSeason.PosterPath;

            Episodes = apiSeason.Episodes.Select(e => new Episode(e)).ToList();
        }

        public virtual ICollection<Episode> Episodes { get; set; }

        public virtual Series Series { get; set; }
    }
}
