namespace MediaCloud.Domain.Entities {

    public class Episode {

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual Season Season { get; set; }
    }
}
