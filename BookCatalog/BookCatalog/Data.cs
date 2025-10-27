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

        // Holds the next available unique Book ID.
        // Using this counter avoids calling _books.Max(b => b.ID) every time a book is added,
        // which would become slow with a large number of records.
        private static int _nextBookId = _books.Max(b => b.ID) + 1;

        // Returns all books
        public static IEnumerable<Book> GetAllBooks() => _books;

        // Returns an author by ID, or null if not found
        public static Author? GetAuthorById(int id)
        {
            return _authors.FirstOrDefault(a => a.ID == id);
        }

        // Returns all books by a specific author
        public static IEnumerable<Book> GetAllBooksByAuthorId(int id)
        {
            return _books.Where(b => b.AuthorID == id);
        }

        // Adds a new book to the list and assigns a unique ID
        public static void AddBook(Book book)
        {
            book.ID = _nextBookId++;
            _books.Add(book);
        }
    }
}
