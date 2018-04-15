using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public class Library<T> where T : Item {

        public string Name { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
