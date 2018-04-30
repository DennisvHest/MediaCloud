using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public abstract class Library {

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ItemLibrary> ItemLibraries { get; set; }
    }
}
