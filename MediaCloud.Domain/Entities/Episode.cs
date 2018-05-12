using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public class Episode {

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual Season Season { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
