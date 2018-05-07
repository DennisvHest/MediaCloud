using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public class Season {

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Episode> Episodes { get; set; }

        public virtual Series Series { get; set; }
    }
}
