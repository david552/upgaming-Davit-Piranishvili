namespace BookCatalog.Dtos
{

    // Used to shape the data sent to clients via API.
    // Includes AuthorName to avoid exposing internal AuthorID.
    public class BookDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int PublicationYear { get; set; }
    }
}
