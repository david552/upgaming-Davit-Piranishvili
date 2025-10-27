using BookCatalog;
using BookCatalog.Dtos;

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



// Returns a list of all books, including their author names.
// If no books exist, returns 404 Not Found.
app.MapGet("/api/books", () =>
{
    var books = Data.GetAllBooks()
 .Select(b => new BookDto
 {
     ID = b.ID,
     Title = b.Title,
     AuthorName = Data.GetAuthorById(b.AuthorID).Name, // every book is guaranteed to have an existing author
     PublicationYear = b.PublicationYear
 });

    if (!books.Any())
    {
        return Results.NotFound($"There is No books");
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


app.Run();