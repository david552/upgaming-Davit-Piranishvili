using BookCatalog;
using BookCatalog.Dtos;
using BookCatalog.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



// Option A: Advanced Filtering & Sorting
// This endpoint returns a list of all books with their author names.
// It also supports optional filtering and sorting via query parameters.
app.MapGet("api/books", (int? publicationYear, string? sortby) =>
{
    var books = Data.GetAllBooks()
    .Select(b => new BookDto()
    {
        ID = b.ID,
        Title = b.Title,
        AuthorName = Data.GetAuthorById(b.AuthorID).Name,
        PublicationYear = b.PublicationYear
    });

    //filter by year
    if (publicationYear.HasValue)
    {
        books = books.Where(b => b.PublicationYear == publicationYear);
    }

    //sort by title
    if (!string.IsNullOrWhiteSpace(sortby))
    {
        if (sortby.Equals("title", StringComparison.OrdinalIgnoreCase))
        {
            books = books.OrderBy(b => b.Title);
        }
        else if (sortby.Equals("year", StringComparison.OrdinalIgnoreCase))
        {
            books = books.OrderBy(b => b.PublicationYear);
        }
    }
    if (!books.Any())
    {
        return Results.NotFound("No books found!");
    }
    return Results.Ok(books);

});



// Returns all books by a specific author.
// Returns 404 if author not found or author has no books.
app.MapGet("/api/authors/{id}/books", (int id) =>
{
    var author = Data.GetAuthorById(id);
    if (author == null)
    {
        return Results.NotFound($"Author with ID {id} not found.");
    }
    var books = Data.GetAllBooksByAuthorId(id)
    .Select(b => new BookDto
    {
        ID = b.ID,
        Title = b.Title,
        AuthorName = author.Name,
        PublicationYear = b.PublicationYear
    });

    if (!books.Any())
    {
        return Results.NotFound($"No books found for author with ID {id}.");
    }

    return Results.Ok(books);
});



// Accepts a new book creation request. Validates:
// 1. Title is not empty
// 2. PublicationYear is not in the future
// 3. Author exists
// Returns 201 Created with a success message if valid.
app.MapPost("api/books", (BookCreateDto newBook) =>
{
    if (string.IsNullOrEmpty(newBook.Title))
    {
        return Results.BadRequest("Title can't be empty");
    }
    if (newBook.PublicationYear > DateTime.Now.Year)
    {
        return Results.BadRequest($"Publication year can not be more than {DateTime.Now.Year}");
    }
    var author = Data.GetAuthorById(newBook.AuthorID);
    if (author == null)
    {
        return Results.BadRequest($"Author with ID {newBook.AuthorID} does not exists");
    }
    // Adding book
    Book book = new Book()
    {
        Title = newBook.Title,
        AuthorID = newBook.AuthorID,
        PublicationYear = newBook.PublicationYear
    };
    Data.AddBook(book);
    return Results.Json(
        new { message = $"Book '{newBook.Title}' successfully added!" },
        statusCode: StatusCodes.Status201Created
    );
});

app.Run();