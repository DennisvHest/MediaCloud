namespace MediaCloud.Domain.Entities {

    public class ItemLibrary {

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
    }
}
