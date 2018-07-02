using System;

namespace MediaCloud.Domain.Entities {

    public class Media {

        public int Id { get; set; }
        public string FileLocation { get; set; }
        public DateTime DateAdded { get; set; }
        
        public virtual Episode Episode { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual Library Library { get; set; }

        public Media() {
            DateAdded = DateTime.Now;
        }
    }
}
