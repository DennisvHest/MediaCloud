using System.IO;

namespace MediaCloud.Common.Models {

    public class TvMediaSearchModel {

        public string Title { get; set; }
        public FileInfo FileInfo { get; set; }
        public SeasonEpisodePair SeasonEpisodePair { get; set; }
    }
}
