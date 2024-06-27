using Books.API.Models;
using HotChocolate;

namespace Books.API.Services;

public class Mutation
{
    public async Task<Book> AddBook(Book book, [Service] IBookService bookService)
    {
        await bookService.AddBookAsync(book);
        return book;
    }

    public async Task<Book> UpdateBook(Book book, [Service] IBookService bookService)
    {
        await bookService.UpdateBookAsync(book);
        return book;
    }

    public async Task<bool> DeleteBook(int id, [Service] IBookService bookService)
    {
        return await bookService.DeleteBookAsync(id);
    }
}