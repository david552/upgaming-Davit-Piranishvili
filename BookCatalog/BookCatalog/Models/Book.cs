namespace BookCatalog.Models
{
    // Represents a book in the system. 
    // Includes the foreign key AuthorID to link to the author.
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int PublicationYear { get; set; }
    }
}
