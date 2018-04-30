namespace MediaCloud.Domain.Entities {

    public class ItemLibrary {

        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
