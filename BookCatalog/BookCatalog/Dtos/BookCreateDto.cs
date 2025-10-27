namespace BookCatalog.Dtos
{

    // Used to accept book creation requests from clients.

    public class BookCreateDto
    {
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int PublicationYear { get; set; }
    }
}
