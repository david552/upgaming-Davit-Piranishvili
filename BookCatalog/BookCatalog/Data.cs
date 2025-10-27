using BookCatalog.Models;

namespace BookCatalog
{

    // Static class to hold in-memory data for Authors and Books.
    // In a real application, this would be replaced by a database.
    public class Data
    {

        // Pre-populated list of authors
        private static List<Author> _authors = new List<Author>
        {
            new Author { ID = 1, Name = "Robert C. Martin" },
            new Author { ID = 2, Name = "Jeffrey Richter" }
        };


        // Pre-populated list of books
        private static List<Book> _books = new List<Book>
        {
            new Book { ID = 1, Title = "Clean Code", AuthorID = 1, PublicationYear = 2008},
            new Book { ID = 2, Title = "CLR via C#", AuthorID = 2, PublicationYear = 2012},
            new Book { ID = 3, Title = "The Clean Coder", AuthorID = 1, PublicationYear = 2011 }
        };


        // Returns all books
        public static IEnumerable<Book> GetAllBooks() => _books;


        // Returns an author by ID, or null if not found
        public static Author? GetAuthorById(int id)
        {
            return _authors.FirstOrDefault(a => a.ID == id);
        }
    }
}
