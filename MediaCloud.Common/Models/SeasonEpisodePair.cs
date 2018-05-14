using System.Text.RegularExpressions;

namespace MediaCloud.Common.Models {

    public class SeasonEpisodePair {

        public int Season { get; set; }
        public int Episode { get; set; }

        public SeasonEpisodePair(string seNotation) {
            Regex regex = new Regex(@"[Ss]([0-9]{1,})[Ee]([0-9]{1,})");
            Match match = regex.Match(seNotation);

            Season = int.Parse(match.Groups[1].Value);
            Episode = int.Parse(match.Groups[2].Value);
        }
    }
}
