using Books.API.Models;
using Books.API.Services;
using HotChocolate;

namespace Books.API.Queries;

public class BookQuery
{
    [UseFiltering]
    [UseSorting]
    public Task<IEnumerable<Book>> GetBooks([Service] IBookService bookService)
    {
        return bookService.GetAllBooksAsync();
    }

    public Task<Book?> GetBookById(int id, [Service] IBookService bookService)
    {
        return bookService.GetBookByIdAsync(id);
    }
}