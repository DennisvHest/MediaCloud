using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public class Genre {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ItemGenre> ItemGenres { get; set; }

        public Genre() { }

        public Genre(TMDbLib.Objects.General.Genre apiGenre) {
            Id = apiGenre.Id;
            Name = apiGenre.Name;
        }
    }
}
