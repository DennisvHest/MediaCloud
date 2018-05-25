namespace MediaCloud.Domain.Entities {

    public class ItemGenre {

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
