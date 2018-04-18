using System.Collections.Generic;

namespace MediaCloud.Domain.Entities {

    public class Library<T> where T : Item {

        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
