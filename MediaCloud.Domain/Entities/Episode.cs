using System.Collections.Generic;
using TMDbLib.Objects.Search;

namespace MediaCloud.Domain.Entities {

    public class Episode {

        public int Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string StillPath { get; set; }

        public Episode() { }

        public Episode(TvSeasonEpisode apiEpisode) {
            Id = apiEpisode.Id;
            EpisodeNumber = apiEpisode.EpisodeNumber;
            Title = apiEpisode.Name;
            StillPath = apiEpisode.StillPath;
        }

        public virtual Season Season { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
